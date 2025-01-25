using BankAccountTransactions.Domain.Repository;
using BankAccountTransactions.Domain.Entity;

namespace BankAccountTransactions.Application.UseCase
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Execute(User user)
        {
           await _userRepository.Insert(user);
            return user;
        }
    }
}

