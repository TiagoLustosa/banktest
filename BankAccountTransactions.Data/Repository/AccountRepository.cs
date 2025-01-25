using BankAccountTransactions.Data.Context;
using BankAccountTransactions.Domain.Repository;
using BankAccountTransactions.Domain;
using BankAccountTransactions.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankAccountTransactions.Data.Repository  
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BankAccountTransactionsContext context) : base(context)
        {
        }
        public async Task<decimal> GetUserBalance(string userDocument)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.CustomerDocument == userDocument);

            if (account == null)
                throw new InvalidOperationException("Account not found for the specified user.");

            return account.Balance;
        }
        public async Task<decimal> AddBalance(string userDocument, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.CustomerDocument == userDocument);

            if (account == null)
                throw new InvalidOperationException("Account not found for the specified user.");

            account.Balance += amount;

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return amount;
        }      
    }
}