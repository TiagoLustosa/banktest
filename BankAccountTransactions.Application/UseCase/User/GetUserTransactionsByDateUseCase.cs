﻿using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;

namespace BankAccountTransactions.Application.UseCase
{
    public class GetUserTransactionsByDateUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserTransactionsByDateUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Transaction>> Execute(string userDocument, DateTime initalDate, DateTime finalDate)
        {
            return await _userRepository.GetUserTransactionsByDate(userDocument, initalDate, finalDate);  
        }
    }
}
