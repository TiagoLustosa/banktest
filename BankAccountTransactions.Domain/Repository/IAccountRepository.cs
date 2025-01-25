using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain.Repository
{
    public interface IAccountRepository: IRepository<Account>
    {
        public Task<decimal> GetUserBalance(string userDocument);
        public Task<decimal> AddBalance(string userDocument, decimal amount);
    }
}
