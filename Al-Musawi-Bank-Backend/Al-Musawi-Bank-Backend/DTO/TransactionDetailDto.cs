namespace Al_Musawi_Bank_Backend.Models
{
    public class TransactionDetailDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        // Other relevant details of a transaction
    }
}