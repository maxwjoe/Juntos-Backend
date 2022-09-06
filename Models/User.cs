using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class User
    {
        // --- Intrinsic Properties ---

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string Description { get; set; }

        public string UserRole { get; set; }

        public string ProfilePictureURL { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // --- Relationships ---

        [ForeignKey("Club")]
        public int AssociatedClubId { get; set; }

        [ForeignKey("Membership")]
        public int AssociatedMembershipId { get; set; }
    }
}