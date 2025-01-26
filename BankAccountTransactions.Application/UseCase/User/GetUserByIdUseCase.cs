using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;

namespace BankAccountTransactions.Application.UseCase
{

    public class GetUserByDocumentUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByDocumentUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Execute(string userDocument)
        {
            return await _userRepository.GetByDocument(userDocument) ?? throw new AccessViolationException("User not found");
        }
    }
}

