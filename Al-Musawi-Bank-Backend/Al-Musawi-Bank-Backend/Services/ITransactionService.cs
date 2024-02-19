namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using System.Threading.Tasks;

public interface ITransactionService
{
    Task<ServiceResponse<TransactionDetailDto>> CreateTransactionAsync(TransactionCreationDto transactionDto);
    Task<TransactionDetailDto> GetTransactionAsync(int transactionId);
    // Other methods as necessary
}