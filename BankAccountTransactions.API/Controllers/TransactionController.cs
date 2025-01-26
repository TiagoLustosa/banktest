
using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BankAccountTransactions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly CreateTransactionUseCase _createTransactionUseCase;


        public TransactionController(
            CreateTransactionUseCase createTransactionUseCase)
        {
            _createTransactionUseCase = createTransactionUseCase;

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            try
            {

                var transaction = await _createTransactionUseCase.Execute(transactionDto);


                return Created("/api/transaction", transaction);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred during the transaction." });
            }
        }
    }
}