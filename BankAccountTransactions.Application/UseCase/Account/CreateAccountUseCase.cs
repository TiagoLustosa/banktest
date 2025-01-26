using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Application.UseCase
{
    public class CreateAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Execute(Account account)
        {
            if (account == null || account.Document == string.Empty)
                throw new ArgumentException("Account or document cannot be null or empty.");

            var existingAccount = await _accountRepository.GetByDocument(account.Document);
            if (existingAccount != null)
                throw new InvalidOperationException("User already has an account.");

        
     

            await _accountRepository.Insert(account);

            return account;
        }
    }

}
