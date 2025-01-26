using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountTransactions.Application.UseCase
{
    public class ValidateUserCredentialsUseCase
    {
        private readonly GetUserByDocumentUseCase _getUserByDocumentUseCase;

        public ValidateUserCredentialsUseCase(GetUserByDocumentUseCase getUserByDocumentUseCase)
        {
            _getUserByDocumentUseCase = getUserByDocumentUseCase;
        }

        public async Task<bool> Execute(LoginRequestDto loginRequestDto)
        {


            if (string.IsNullOrWhiteSpace(loginRequestDto.Document) || string.IsNullOrWhiteSpace(loginRequestDto.Password))
                throw new ArgumentException("Email and password must not be empty.");

            var user = await _getUserByDocumentUseCase.Execute(loginRequestDto.Document);

            if (user != null && user.Password == loginRequestDto.Password)
            {
                return true;
            }

            return false;
        }
    }
}
