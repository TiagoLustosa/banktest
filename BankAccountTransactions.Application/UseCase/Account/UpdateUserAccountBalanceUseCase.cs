using BankAccountTransactions.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Application.UseCase
{
    public class UpdateUserAccountBalanceUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateUserAccountBalanceUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> ExecuteAsync(string userDocument, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("Account document cannot be empty.", nameof(userDocument));

            var userAccount = await _accountRepository.GetByDocument(userDocument);

            if (userAccount == null)
                throw new Exception("User not found.");

            userAccount.Balance += amount;
            await _accountRepository.Update(userAccount);

            return userAccount.Balance;
        }
    }
}
