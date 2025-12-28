using BuildingBlocks.Abstractions.Interfaces;
using MediatR;
using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.SendEmail
{
    public sealed class SendEmailHandler : IRequestHandler<SendEmailCommand, int>
    {
        private readonly IEmailMessageRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IClock _clock;

        public SendEmailHandler(IEmailMessageRepository repo, IUnitOfWork uow, IClock clock)
        {
            _repo = repo;
            _uow = uow;
            _clock = clock;
        }

        public async Task<int> Handle(SendEmailCommand request, CancellationToken ct)
        {
            var now = _clock.Now;

            var message = new EmailMessage
            {
                UserId = request.UserId,
                To = request.To.Trim(),
                Subject = request.Subject.Trim(),
                Body = request.Body,
                Status = EmailStatus.Pending,
                Attempts = 0,
                Created = now,
                NextAttemptAt = now,
                CorrelationId = request.CorrelationId
            };

            await _repo.AddAsync(message, ct);
            await _uow.SaveChangesAsync(ct);

            return message.Id;
        }
    }
}
