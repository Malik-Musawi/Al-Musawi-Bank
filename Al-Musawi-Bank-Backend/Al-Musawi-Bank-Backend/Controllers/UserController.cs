namespace Al_Musawi_Bank_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Al_Musawi_Bank_Backend.Models;
using Al_Musawi_Bank_Backend.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto registration)
    {
        var result = await _userService.RegisterUserAsync(registration);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto login)
    {
        var result = await _userService.LoginUserAsync(login);
        if (result.IsSuccess)
        {
            return Ok(result.Data); // This now contains both token and user data
        }
        return Unauthorized(result.Message);
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto updateDto)
    {
        var result = await _userService.UpdateUserAsync(updateDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var result = await _userService.ChangePasswordAsync(changePasswordDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        var profile = await _userService.GetProfileAsync(int.Parse(userId));
        if (profile != null)
            return Ok(profile);

        return NotFound();
    }


    // Add other methods as necessary.
}