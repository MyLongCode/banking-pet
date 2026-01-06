using BuildingBlocks.Abstractions.Models;
using Notifications.Domain.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Domain.Entities
{
    [Table("TelegramMessages", Schema = "notifications")]
    public class TelegramNotification : Notification
    {
        public ParseMode? ParseMode { get; private set; }
        public bool DisableWebPagePreview { get; private set; }
        public bool DisableNotification { get; private set; }

        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; } = null!;

        public TelegramNotification(
            string recipient,          
            string title,
            string message,
            ParseMode? parseMode = Enums.ParseMode.MarkDownV2,
            bool disableWebPagePreview = false,
            bool disableNotification = false)
            : base(recipient, title, message)
        {
            ParseMode = parseMode ?? throw new ArgumentNullException(nameof(parseMode));
            DisableWebPagePreview = disableWebPagePreview;
            DisableNotification = disableNotification;
        }

        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrWhiteSpace(Recipient))
                throw new ArgumentException("Recipient (chat_id) is required.");

            var r = Recipient.Trim();

            var isNumericChatId = long.TryParse(r, out _);
            var isUsernameOrChannel = r.StartsWith("@", StringComparison.Ordinal);

            if (!isNumericChatId && !isUsernameOrChannel)
                throw new ArgumentException("Invalid Telegram recipient. Use numeric chat_id or @username/@channel.");
        }
    }
}
