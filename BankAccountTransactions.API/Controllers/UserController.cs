using BankAccountTransactions.Application.UseCase;
using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountTransactions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly CreateAccountUseCase _createAccountUseCase;
        private readonly GetAllUserTransactionsUseCase _getAllUserTransactionsUseCase;
        private readonly GetUserTransactionsByDateUseCase _getUserTransactionsByDateUseCase;
        private readonly ValidateUserCredentialsUseCase _validateUserCredentialsUseCase;
        private readonly TokenService _tokenService;

        public UserController(
            CreateUserUseCase createUserUseCase,
            CreateAccountUseCase createAccountUseCase,
            GetAllUserTransactionsUseCase getAllUserTransactionsUseCase,
            GetUserTransactionsByDateUseCase getUserTransactionsByDateUseCase,
            ValidateUserCredentialsUseCase validateUserCredentialsUseCase,
            TokenService tokenService)
        {
            _createUserUseCase = createUserUseCase;
            _createAccountUseCase = createAccountUseCase;
            _getAllUserTransactionsUseCase = getAllUserTransactionsUseCase;
            _getUserTransactionsByDateUseCase = getUserTransactionsByDateUseCase;
            _validateUserCredentialsUseCase = validateUserCredentialsUseCase;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> CreateUserAndAccount([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Document))
                return BadRequest("User data is invalid.");


            var account = new Account(userDto.Document, userDto.Balance);


            var createdAccount = await _createAccountUseCase.Execute(account);

            if (createdAccount == null)
                throw new Exception("Fail to create new account");

            var user = new User
            {
                FullName = userDto.FullName,
                Document = userDto.Document,
                Email = userDto.Email,
                AccountId = createdAccount.Id,
                Password = userDto.Password,
                UserType = userDto.UserType,
            };
            var createdUser = await _createUserUseCase.Execute(user);



            return Created("/api/user/create", createdUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Document) || string.IsNullOrWhiteSpace(loginRequest.Password))
                return BadRequest("Document or password cannot be empty.");

            // Validate user credentials
            var user = await _validateUserCredentialsUseCase.Execute(loginRequest);
            if (user == null)
                return Unauthorized("Invalid document or password.");

            // Generate token
            var token = _tokenService.GenerateToken(loginRequest.Document);
            return Ok(new { Token = token });
        }
        [Authorize]
        [HttpGet("transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllUserTransactions(string document)
        {
            var transactions = await _getAllUserTransactionsUseCase.Execute(document);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for the user.");

            return Ok(transactions);
        }
        [Authorize]
        [HttpGet("transactions-by-date")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetUserTransactionsByDate(
            [FromBody] GetTransactionByDateDto getTransactionByDateDto)
        {
            var transactions = await _getUserTransactionsByDateUseCase.Execute(
               getTransactionByDateDto);

            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for the specified date range.");

            return Ok(transactions);
        }      
    }

}