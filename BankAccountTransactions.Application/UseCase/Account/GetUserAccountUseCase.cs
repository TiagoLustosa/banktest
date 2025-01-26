using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;

namespace BankAccountTransactions.Application.UseCase
{
    public class GetUserAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public GetUserAccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> Execute(string userDocument)
        {
            if (string.IsNullOrEmpty(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            Account userAccount = await _accountRepository.GetByDocument(userDocument) ??
                throw new ArgumentException("User account not found", nameof(userAccount));


            return userAccount;
        }
    }
}