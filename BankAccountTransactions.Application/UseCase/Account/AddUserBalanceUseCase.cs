using BankAccountTransactions.Domain.Repository;

namespace BankAccountTransactions.Application.UseCase
{
    public class AddUserBalanceUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public AddUserBalanceUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> Execute(string userDocument, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(userDocument))
                throw new ArgumentException("User document cannot be empty.", nameof(userDocument));

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            return await _accountRepository.AddBalance(userDocument, amount);
        }
    }
}