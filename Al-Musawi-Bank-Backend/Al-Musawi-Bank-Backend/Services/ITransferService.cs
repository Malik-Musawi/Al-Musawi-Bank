namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using System.Threading.Tasks;

public interface ITransferService
{
    Task<ServiceResponse<TransferDetailDto>> CreateTransferAsync(TransferCreationDto transferDto);
    Task<TransferDetailDto> GetTransferAsync(int transferId);
    // Other methods as necessary...
}
