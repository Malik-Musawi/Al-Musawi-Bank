namespace Al_Musawi_Bank_Backend.Models
{
    public class AccountUpdateDto
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; } // Example property, depends on what you allow to be updated
        // Other properties as needed
    }
}