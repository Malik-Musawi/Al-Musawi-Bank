namespace Al_Musawi_Bank_Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Al_Musawi_Bank_Backend.Models;
using Al_Musawi_Bank_Backend.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateTransfer([FromBody] TransferCreationDto transferDto)
    {
        var result = await _transferService.CreateTransferAsync(transferDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransfer(int id)
    {
        var transfer = await _transferService.GetTransferAsync(id);
        if (transfer != null)
            return Ok(transfer);

        return NotFound();
    }

    // Additional methods can be implemented as necessary...
}
