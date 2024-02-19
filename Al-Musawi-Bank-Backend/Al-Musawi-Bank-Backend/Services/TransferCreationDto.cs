namespace Al_Musawi_Bank_Backend.Services;

public class TransferCreationDto
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}