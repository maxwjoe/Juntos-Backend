namespace Juntos.Models
{
    public class User
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int MembershipId { get; set; }
        public string UserName { get; set; }
        //NOTE: Emails must be unique
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}