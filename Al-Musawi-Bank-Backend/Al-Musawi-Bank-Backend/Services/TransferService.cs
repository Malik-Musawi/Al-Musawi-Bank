namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class TransferService : ITransferService
{
    private readonly BankDbContext _context;

    public TransferService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<TransferDetailDto>> CreateTransferAsync(TransferCreationDto transferDto)
    {
        var fromAccount = await _context.Accounts.FindAsync(transferDto.FromAccountId);
        var toAccount = await _context.Accounts.FindAsync(transferDto.ToAccountId);

        if (fromAccount == null || toAccount == null)
        {
            return new ServiceResponse<TransferDetailDto>
            {
                IsSuccess = false,
                Message = "Account not found."
            };
        }

        if (fromAccount.Balance < transferDto.Amount)
        {
            return new ServiceResponse<TransferDetailDto>
            {
                IsSuccess = false,
                Message = "Insufficient funds."
            };
        }

        var transfer = new Transfer
        {
            FromAccountId = transferDto.FromAccountId,
            ToAccountId = transferDto.ToAccountId,
            Amount = transferDto.Amount,
            Date = DateTime.UtcNow,
            Description = transferDto.Description
        };

        fromAccount.Balance -= transferDto.Amount;
        toAccount.Balance += transferDto.Amount;

        _context.Transfers.Add(transfer);
        await _context.SaveChangesAsync();

        var responseDto = new TransferDetailDto
        {
            TransferId = transfer.TransferId,
            FromAccountId = transfer.FromAccountId,
            ToAccountId = transfer.ToAccountId,
            Amount = transfer.Amount,
            Date = transfer.Date,
            Description = transfer.Description
        };

        return new ServiceResponse<TransferDetailDto>
        {
            Data = responseDto,
            IsSuccess = true,
            Message = "Transfer completed successfully."
        };
    }

    public async Task<TransferDetailDto> GetTransferAsync(int transferId)
    {
        var transfer = await _context.Transfers
            .Include(t => t.FromAccount)
            .Include(t => t.ToAccount)
            .FirstOrDefaultAsync(t => t.TransferId == transferId);

        if (transfer == null)
        {
            return null;
        }

        return new TransferDetailDto
        {
            TransferId = transfer.TransferId,
            FromAccountId = transfer.FromAccountId,
            ToAccountId = transfer.ToAccountId,
            Amount = transfer.Amount,
            Date = transfer.Date,
            Description = transfer.Description
        };
    }

    // Additional methods and logic can be added as necessary...
}