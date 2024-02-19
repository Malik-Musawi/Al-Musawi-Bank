namespace Al_Musawi_Bank_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Al_Musawi_Bank_Backend.Models;
using Al_Musawi_Bank_Backend.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount(AccountCreationDto accountCreation)
    {
        var result = await _accountService.CreateAccountAsync(accountCreation);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [Authorize]
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccount(int accountId)
    {
        var result = await _accountService.GetAccountAsync(accountId);
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAccount(AccountUpdateDto accountUpdate)
    {
        var result = await _accountService.UpdateAccountAsync(accountUpdate);
        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    // Additional account-related methods...
}
