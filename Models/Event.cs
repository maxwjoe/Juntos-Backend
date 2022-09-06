
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class Event
    {
        // --- Intrinsic Properties ---

        public int Id { get; set; }

        public int CapacityLimit { get; set; }

        public int BookingLimitMinutes { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateAndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }



        // --- Relationships ---

        public User Owner { get; set; }
        public Club AssociatedClub { get; set; }

        public ICollection<Membership> AllowedMemberships { get; set; }
        public ICollection<User> BookedUsers { get; set; }

    }
}