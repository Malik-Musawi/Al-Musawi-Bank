namespace Al_Musawi_Bank_Backend.Models
{
    public class ChangePasswordDto
    {
        // user id
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}