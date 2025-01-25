using System.Reflection;
using BankAccountTransactions.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
