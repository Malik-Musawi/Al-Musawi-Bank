namespace Al_Musawi_Bank_Backend.Models
{
    public class TransactionCreationDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        // Other relevant properties for a transaction creation
    }
}