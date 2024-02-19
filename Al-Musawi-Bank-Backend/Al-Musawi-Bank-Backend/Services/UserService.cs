namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;


public class UserService : IUserService
{
    private readonly BankDbContext _context;
    private readonly TokenService _tokenService;

    public UserService(BankDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<ServiceResponse<UserRegistrationDto>> RegisterUserAsync(UserRegistrationDto registrationDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registrationDto.Email))
        {
            return new ServiceResponse<UserRegistrationDto>
            {
                IsSuccess = false,
                Message = "User already exists with the given email."
            };
        }

        CreatePasswordHash(registrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User newUser = new User
        {
            Email = registrationDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Name = registrationDto.Name
            // Map other properties from registrationDto to User
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return new ServiceResponse<UserRegistrationDto>
        {
            Data = registrationDto,
            IsSuccess = true,
            Message = "User registered successfully."
        };
    }


    public async Task<ServiceResponse<LoginResponseDto>> LoginUserAsync(UserLoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResponse<LoginResponseDto>
            {
                IsSuccess = false,
                Message = "User not found or password does not match."
            };
        }

        var token = _tokenService.GenerateToken(user);
        var userData = new UserDto // Assuming you have a UserDto that represents user data
        {
            // Map properties from user to UserDto
            UserId = user.UserId,
            Email = user.Email,
            Name = user.Name
        };

        var loginResponse = new LoginResponseDto
        {
            Token = token,
            UserData = userData
        };

        return new ServiceResponse<LoginResponseDto>
        {
            Data = loginResponse,
            IsSuccess = true,
            Message = "Login successful."
        };
    }

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await _context.Users.FindAsync(changePasswordDto.UserId);
        if (user == null || !VerifyPasswordHash(changePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "User not found or old password does not match."
            };
        }

        CreatePasswordHash(changePasswordDto.NewPassword, out byte[] newHash, out byte[] newSalt);
        user.PasswordHash = newHash;
        user.PasswordSalt = newSalt;
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            IsSuccess = true,
            Message = "Password changed successfully."
        };
    }

    public async Task<ServiceResponse<UserUpdateDto>> UpdateUserAsync(UserUpdateDto updateDto)
    {
        var user = await _context.Users.FindAsync(updateDto.UserId);
        if (user == null)
        {
            return new ServiceResponse<UserUpdateDto>
            {
                IsSuccess = false,
                Message = "User not found."
            };
        }

        // Update user properties
        user.Name = updateDto.Name;
        user.Email = updateDto.Email;
        // Map other properties from updateDto to User

        await _context.SaveChangesAsync();

        return new ServiceResponse<UserUpdateDto>
        {
            Data = updateDto,
            IsSuccess = true,
            Message = "User updated successfully."
        };
    }

    public async Task<UserProfileDto> GetProfileAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null; // Or handle it differently based on your design
        }

        return new UserProfileDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email
            // Map other properties from User to UserProfileDto
        };
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jIkIg+dvyCeuJ0SJWWMnwCsHa7EmKElDS7+GJoJPfmdFfhF9LH5W3m2K7+0KbIdCUg4NM3dsEZ09YcFAJehIgg==\n"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            // Add other claims as needed
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


}
