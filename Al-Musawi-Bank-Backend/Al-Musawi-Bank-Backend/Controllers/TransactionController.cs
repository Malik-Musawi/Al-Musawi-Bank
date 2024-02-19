namespace Al_Musawi_Bank_Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Al_Musawi_Bank_Backend.Models;
using Al_Musawi_Bank_Backend.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTransaction(TransactionCreationDto transactionDto)
    {
        var result = await _transactionService.CreateTransactionAsync(transactionDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var transaction = await _transactionService.GetTransactionAsync(id);
        if (transaction != null)
            return Ok(transaction);

        return NotFound();
    }

    // Implement other necessary endpoints...
}
