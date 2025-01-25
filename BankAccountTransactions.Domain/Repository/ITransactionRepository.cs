using BankAccountTransactions.Domain.Interface;
using BankAccountTransactionsDomain.Entity;


namespace BankAccountTransactions.Domain.Repository
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}
