namespace Al_Musawi_Bank_Backend.Services;
using Al_Musawi_Bank_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class TransactionService : ITransactionService
{
    private readonly BankDbContext _context;

    public TransactionService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<TransactionDetailDto>> CreateTransactionAsync(TransactionCreationDto transactionDto)
    {
        var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
        if (account == null)
        {
            return new ServiceResponse<TransactionDetailDto>
            {
                IsSuccess = false,
                Message = "Account not found."
            };
        }

        var transaction = new Transaction
        {
            AccountId = transactionDto.AccountId,
            Amount = transactionDto.Amount,
            Date = DateTime.UtcNow, // Assuming transaction date is the current time
            Description = transactionDto.Description
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        var responseDto = new TransactionDetailDto
        {
            TransactionId = transaction.TransactionId,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description
        };

        return new ServiceResponse<TransactionDetailDto>
        {
            Data = responseDto,
            IsSuccess = true,
            Message = "Transaction created successfully."
        };
    }

    public async Task<TransactionDetailDto> GetTransactionAsync(int transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);

        if (transaction == null)
        {
            return null;
        }

        return new TransactionDetailDto
        {
            TransactionId = transaction.TransactionId,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description
        };
    }

    // Implement other methods as necessary...
}
