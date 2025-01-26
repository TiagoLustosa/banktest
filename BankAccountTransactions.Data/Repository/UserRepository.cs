
using BankAccountTransactions.Data.Context;
using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankAccountTransactions.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BankAccountTransactionsContext context) : base(context) { }

        public async Task<decimal> GetUserBalance(string userDocument)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Document == userDocument);

            if (account == null)
                throw new InvalidOperationException("Account not found for the specified user.");

            return account.Balance;
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsByDate(GetTransactionByDateDto getTransactionByDateDto)
        {
            if (getTransactionByDateDto == null)
                throw new ArgumentException("Cannot found data check dates and document.");

            if (getTransactionByDateDto.InitialDate > getTransactionByDateDto.FinalDate)
                throw new ArgumentException("Initial date cannot be after the final date.");

            return await _context.Transactions
                .Where(t => (t.SenderDocument == getTransactionByDateDto.UserDocument || t.ReceiverDocument == getTransactionByDateDto.UserDocument) && t.TransactionDate >= getTransactionByDateDto.InitialDate && t.TransactionDate <= getTransactionByDateDto.FinalDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetAllUserTransactions(string userDocument)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            return await _context.Transactions
                .Where(t => (t.SenderDocument == userDocument || t.ReceiverDocument == userDocument)).ToListAsync();
        }

    }
}