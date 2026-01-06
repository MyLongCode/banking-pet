using Microsoft.Extensions.Options;
using Notifications.Application.Abstractions;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Infrastructure.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Services
{
    public sealed class TelegramSender : ITelegramSender
    {
        private readonly TelegramOptions _opt;

        public TelegramSender(IOptions<TelegramOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(TelegramNotification notification)
        {
            notification.Validate();

            var chatId = string.IsNullOrWhiteSpace(notification.Recipient)
                ? _opt.DefaultChatId
                : notification.Recipient;

            if (string.IsNullOrWhiteSpace(chatId))
                throw new InvalidOperationException("Telegram chat_id is not specified (Recipient/DefaultChatId).");

            var text = string.IsNullOrWhiteSpace(notification.Title)
                ? notification.Message
                : $"<b>{notification.Title}</b>\n\n{notification.Message}";

            using var http = new HttpClient
            {
                BaseAddress = new Uri(_opt.ApiBaseUrl.TrimEnd('/') + "/")
            };

            var url = $"bot{_opt.BotToken}/sendMessage";

            var payload = new SendMessageRequest
            {
                ChatId = chatId!,
                Text = text,
                ParseMode = notification.ParseMode,
                DisableWebPagePreview = notification.DisableWebPagePreview,
                DisableNotification = notification.DisableNotification
            };

            using var response = await http.PostAsJsonAsync(url, payload);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Telegram sendMessage failed: {(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
            }
        }

        private sealed class SendMessageRequest
        {
            [JsonPropertyName("chat_id")]
            public string ChatId { get; init; } = default!;

            [JsonPropertyName("text")]
            public string Text { get; init; } = default!;

            [JsonPropertyName("parse_mode")]
            public ParseMode? ParseMode { get; init; }

            [JsonPropertyName("disable_web_page_preview")]
            public bool DisableWebPagePreview { get; init; }

            [JsonPropertyName("disable_notification")]
            public bool DisableNotification { get; init; }
        }
    }
}
