namespace Al_Musawi_Bank_Backend.Services;

public class TransferDetailDto
{
    public int TransferId { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}