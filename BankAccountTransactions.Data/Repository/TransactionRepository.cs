using BankAccountTransactions.Data.Context;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankAccountTransactions.Data.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankAccountTransactionsContext context) : base(context) { }
                
    }
}