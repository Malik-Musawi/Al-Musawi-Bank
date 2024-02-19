namespace Al_Musawi_Bank_Backend.Models
{
    public class AccountDetailDto
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        // Other relevant details like account type, creation date, etc.
    }
}