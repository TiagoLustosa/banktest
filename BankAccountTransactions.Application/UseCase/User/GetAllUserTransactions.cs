using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;

namespace BankAccountTransactions.Application.UseCase
{
    public class GetAllUserTransactionsUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserTransactionsUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Transaction>> Execute(string userDocument)
        {
            return await _userRepository.GetAllUserTransactions(userDocument);
        }
    }
}
