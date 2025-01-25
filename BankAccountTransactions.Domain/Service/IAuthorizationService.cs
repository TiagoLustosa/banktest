namespace BankAccountTransactions.Domain.Service
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeTransactionAsync();
    }
}