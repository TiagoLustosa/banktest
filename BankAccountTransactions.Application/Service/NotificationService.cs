using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Service;
using System.Net.Http.Json;

namespace BankAccountTransactions.Application.Service
{
    public class NotificationService : INotificationService
    {
        private const string NotificationUrl = "https://util.devi.tools/api/v1/notify";
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendNotificationAsync(User user, string message)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be empty.", nameof(message));

            var notificationRequest = new NotificationDto(user.Email, message);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(NotificationUrl, notificationRequest);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to send notification.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Notification error: {ex.Message}");
            }
        }
    }
}
