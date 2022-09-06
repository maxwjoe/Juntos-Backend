
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class Membership
    {
        // --- Intrinsic Properties ---

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double? Price { get; set; }

        public int BillingOption { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }



        // --- Relationships ---

        public Club associatedClub { get; set; }

        public ICollection<User> associatedUsers { get; set; }



    }
}