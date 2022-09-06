
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class User
    {
        // --- Object Details ---

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserRole { get; set; }

        public string ProfilePictureURL { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // --- Relationships ---

        public Club associatedClub { get; set; }

        public Membership userMembership { get; set; }


    }
}