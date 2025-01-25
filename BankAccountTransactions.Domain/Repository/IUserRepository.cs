using BankAccountTransactions.Domain.Interface;
using BankAccountTransactionsDomain.Entity;

namespace BankAccountTransactions.Domain.Repository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
