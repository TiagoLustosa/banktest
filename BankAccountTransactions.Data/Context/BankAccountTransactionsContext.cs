using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BankAccountTransactionsDomain.Entity;
using BankAccountTransactions.Domain;

namespace BankAccountTransactions.Data.Context
{
    public class BankAccountTransactionsContext : DbContext
    {
        public BankAccountTransactionsContext(DbContextOptions<BankAccountTransactionsContext> context) : base(context)
        {
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<Transaction> Transactions { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
