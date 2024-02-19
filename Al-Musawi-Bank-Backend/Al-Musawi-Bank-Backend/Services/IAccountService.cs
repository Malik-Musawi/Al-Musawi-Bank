namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using System.Threading.Tasks;

public interface IAccountService
{
    Task<ServiceResponse<AccountCreationDto>> CreateAccountAsync(AccountCreationDto accountCreationDto);
    Task<AccountDetailDto> GetAccountAsync(int accountId); // AccountDetailDto needs to be defined
    Task<ServiceResponse<AccountUpdateDto>> UpdateAccountAsync(AccountUpdateDto accountUpdateDto);
    // Other methods as necessary
}
