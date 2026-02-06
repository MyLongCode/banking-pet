using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Abstractions;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.Handlers.Notifications
{
    public sealed class CreateBatchNotificationsHandler
    : IRequestHandler<CreateBatchNotificationsCommand, BatchNotificationResult>
    {
        private readonly INotificationRepository _repo;
        private readonly INotificationFactoryResolver _factoryResolver;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateBatchNotificationsHandler> _logger;

        public CreateBatchNotificationsHandler(
            INotificationRepository repo,
            INotificationFactoryResolver factoryResolver,
            IUnitOfWork unitOfWork,
            ILogger<CreateBatchNotificationsHandler> logger)
        {
            _repo = repo;
            _factoryResolver = factoryResolver;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BatchNotificationResult> Handle(
            CreateBatchNotificationsCommand request,
            CancellationToken ct)
        {
            var batchId = Guid.NewGuid();
            var stopwatch = Stopwatch.StartNew();
            var results = new List<BatchNotificationItemResult>();

            try
            {
                if (request.UseTransaction)
                    await _unitOfWork.BeginTransactionAsync(ct);

                var options = new ParallelOptions
                {
                    CancellationToken = ct,
                    MaxDegreeOfParallelism = request.MaxDegreeOfParallelism ?? Environment.ProcessorCount
                };

                var notifications = new ConcurrentBag<Notification>();

                await Parallel.ForEachAsync(
                    request.Items,
                    options,
                    async (item, itemCt) =>
                    {
                        var itemStopwatch = Stopwatch.StartNew();
                        var itemResult = new BatchNotificationItemResult
                        {
                            Type = item.Type,
                            Recipient = item.Recipient
                        };

                        try
                        {
                            var factory = _factoryResolver.Resolve(item.Type);
                            var notification = factory.CreateNotification(
                                item.Type, item.Recipient, item.Title,
                                item.Message);

                            notification.Validate();
                            await factory.SendAsync(notification, itemCt);

                            notifications.Add(notification);

                            itemResult = itemResult with
                            {
                                NotificationId = notification.Id,
                                IsSuccess = true,
                                Duration = itemStopwatch.Elapsed
                            };
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex,
                                "Failed to create notification for {Recipient}",
                                item.Recipient);

                            itemResult = itemResult with
                            {
                                IsSuccess = false,
                                ErrorMessage = ex.Message,
                                Duration = itemStopwatch.Elapsed
                            };
                        }
                        finally
                        {
                            lock (results) results.Add(itemResult);
                        }
                    });

                if (notifications.Any())
                {
                    await _repo.AddRangeAsync(notifications, ct);
                }

                if (request.UseTransaction)
                    await _unitOfWork.CommitTransactionAsync(ct);

                return new BatchNotificationResult
                {
                    BatchId = batchId,
                    TotalCount = request.Items.Count,
                    SuccessCount = results.Count(r => r.IsSuccess),
                    FailedCount = results.Count(r => !r.IsSuccess),
                    Items = results,
                    TotalDuration = stopwatch.Elapsed
                };
            }
            catch (Exception ex)
            {
                if (request.UseTransaction)
                    await _unitOfWork.RollbackTransactionAsync(ct);

                _logger.LogError(ex, "Batch processing failed for batch {BatchId}", batchId);
                throw;
            }
        }
    }
}
