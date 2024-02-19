namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using System.Threading.Tasks;


public interface IUserService
{
    Task<ServiceResponse<UserRegistrationDto>> RegisterUserAsync(UserRegistrationDto registrationDto);
    Task<ServiceResponse<LoginResponseDto>> LoginUserAsync(UserLoginDto loginDto);
    Task<ServiceResponse<UserUpdateDto>> UpdateUserAsync(UserUpdateDto updateDto);
    Task<ServiceResponse<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<UserProfileDto> GetProfileAsync(int userId);


}