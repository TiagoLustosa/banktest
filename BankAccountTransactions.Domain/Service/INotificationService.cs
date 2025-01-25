
using BankAccountTransactions.Domain.Entity;

namespace BankAccountTransactions.Domain.Service
{
    public interface INotificationService
    {
        Task SendNotificationAsync(User user, string message);
    }
}