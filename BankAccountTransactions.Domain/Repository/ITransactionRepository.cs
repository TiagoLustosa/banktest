using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Interface;


namespace BankAccountTransactions.Domain.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
    }
}
