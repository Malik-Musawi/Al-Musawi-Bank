namespace Al_Musawi_Bank_Backend.Models;

public class Transfer
{
    public int TransferId { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public virtual Account FromAccount { get; set; }
    public virtual Account ToAccount { get; set; }
}
