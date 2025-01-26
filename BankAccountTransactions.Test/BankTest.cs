//namespace BankAccountTransactions.Test
//{
  
//    using System.Net;

//    namespace BankTest.Tests
//    {
//        public class AccountControllerTests
//        {
//            private readonly Mock<IAccountService> _mockAccountService;
//            private readonly AccountController _controller;

//            public AccountControllerTests()
//            {
//                _mockAccountService = new Mock<IAccountService>();
//                _controller = new AccountController(_mockAccountService.Object);
//            }

//            [Fact]
//            public async Task CreateAccount_ReturnsCreatedAccount()
//            {
//                // Arrange
//                var accountDto = new AccountDto { Id = 1, Balance = 100 };
//                _mockAccountService.Setup(s => s.CreateAccountAsync(It.IsAny<AccountDto>()))
//                    .ReturnsAsync(accountDto);

//                // Act
//                var result = await _controller.CreateAccount(accountDto);

//                // Assert
//                var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
//                Assert.Equal(accountDto, createdResult.Value);
//            }

//            [Fact]
//            public async Task GetAccount_ExistingAccount_ReturnsAccount()
//            {
//                // Arrange
//                var accountDto = new AccountDto { Id = 1, Balance = 100 };
//                _mockAccountService.Setup(s => s.GetAccountByIdAsync(1))
//                    .ReturnsAsync(accountDto);

//                // Act
//                var result = await _controller.GetAccount(1);

//                // Assert
//                var okResult = Assert.IsType<OkObjectResult>(result.Result);
//                Assert.Equal(accountDto, okResult.Value);
//            }

//            [Fact]
//            public async Task GetAccount_NonExistingAccount_ReturnsNotFound()
//            {
//                // Arrange
//                _mockAccountService.Setup(s => s.GetAccountByIdAsync(1))
//                    .ThrowsAsync(new AccountNotFoundException("Account not found"));

//                // Act
//                var result = await _controller.GetAccount(1);

//                // Assert
//                Assert.IsType<NotFoundResult>(result.Result);
//            }

//            [Fact]
//            public async Task Deposit_ValidAmount_ReturnsUpdatedAccount()
//            {
//                // Arrange
//                var updatedAccount = new AccountDto { Id = 1, Balance = 200 };
//                _mockAccountService.Setup(s => s.DepositAsync(1, 100))
//                    .ReturnsAsync(updatedAccount);

//                // Act
//                var result = await _controller.Deposit(1, 100);

//                // Assert
//                var okResult = Assert.IsType<OkObjectResult>(result.Result);
//                Assert.Equal(updatedAccount, okResult.Value);
//            }

//            [Fact]
//            public async Task Withdraw_ValidAmount_ReturnsUpdatedAccount()
//            {
//                // Arrange
//                var updatedAccount = new AccountDto { Id = 1, Balance = 50 };
//                _mockAccountService.Setup(s => s.WithdrawAsync(1, 50))
//                    .ReturnsAsync(updatedAccount);

//                // Act
//                var result = await _controller.Withdraw(1, 50);

//                // Assert
//                var okResult = Assert.IsType<OkObjectResult>(result.Result);
//                Assert.Equal(updatedAccount, okResult.Value);
//            }

//            [Fact]
//            public async Task Withdraw_InsufficientBalance_ReturnsBadRequest()
//            {
//                // Arrange
//                _mockAccountService.Setup(s => s.WithdrawAsync(1, 500))
//                    .ThrowsAsync(new InsufficientBalanceException("Insufficient balance"));

//                // Act
//                var result = await _controller.Withdraw(1, 500);

//                // Assert
//                Assert.IsType<BadRequestResult>(result.Result);
//            }
//        }
//    }
//}
