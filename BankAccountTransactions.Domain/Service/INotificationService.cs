
using BankAccountTransactions.Domain.Entity;

namespace BankAccountTransactions.Domain.Service
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(User user, string message);
    }
}