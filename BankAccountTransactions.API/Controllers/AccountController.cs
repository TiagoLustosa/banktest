using BankAccountTransactions.Application.UseCase;
using BankAccountTransactions.Domain.Dto;
using BankAccountTransactions.Domain.Entity;
using BankAccountTransactions.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AddUserBalanceUseCase _addUserBalanceUseCase;
    private readonly GetUserBalanceUseCase _getUserBalanceUseCase;

    public AccountController(
        AddUserBalanceUseCase addUserBalanceUseCase,
        GetUserBalanceUseCase getUserBalanceUseCase
        )
    {
        _addUserBalanceUseCase = addUserBalanceUseCase;
        _getUserBalanceUseCase = getUserBalanceUseCase;
    }

    [HttpGet]
    [Authorize]
    [Route("balance")]
    public async Task<ActionResult<decimal>> GetUserBalance([FromBody] DocumentRequestDto document)
    {
        if (string.IsNullOrWhiteSpace(document.Document))
            return BadRequest("Invalid document.");

        try
        {
            var balance = await _getUserBalanceUseCase.Execute(document.Document);
            if (balance == null)
                return NotFound("User not found.");

            return Ok(balance);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error getting balance: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize]
    [Route("add-balance")]
    public async Task<ActionResult> AddUserBalance([FromBody] DocumentAmountRequestDto docAmountDto)
    {
        if (string.IsNullOrWhiteSpace(docAmountDto.Document) || docAmountDto.Amount <= 0)
            return BadRequest("Invalid data.");

        try
        {
            var newBalance = await _addUserBalanceUseCase.Execute(docAmountDto.Document, docAmountDto.Amount);
            if (newBalance == null)
                return NotFound("User not found.");



            return Ok(new
            {
                Message = "Balance updated.",
                NewBalance = newBalance
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error adding balance: {ex.Message}");
        }
    }
}


