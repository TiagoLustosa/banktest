using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Interface;

namespace BankAccountTransactions.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
       Task<IEnumerable<Transaction>> GetUserTransactionsByDate(GetTransactionByDateDto transactionByDateDto);
        Task<IEnumerable<Transaction>> GetAllUserTransactions(string userDocument);
        Task<decimal> GetUserBalance(string userDocument);       

    }
}
