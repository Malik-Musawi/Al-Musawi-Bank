namespace Al_Musawi_Bank_Backend.Models;

public class Account
{
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } // An account can have multiple transactions
}
