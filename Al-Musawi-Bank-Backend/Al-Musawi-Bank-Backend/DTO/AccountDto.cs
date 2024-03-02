namespace Al_Musawi_Bank_Backend.Models
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string AccountNumber { get; set; } // Added missing closing bracket here
        public decimal Balance { get; set; }
        // Any other properties needed for the account summary
    }
}