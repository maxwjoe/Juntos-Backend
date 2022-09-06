
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class Event
    {
        // --- Intrinsic Properties ---

        public int Id { get; set; }

        public int CapacityLimit { get; set; }

        public int BookingTimeLimitMinutes { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public bool DoesRepeat { get; set; }

        public int RepeatOption { get; set; }

        public DateTime EventDateAndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }



        // --- Relationships ---

        [ForeignKey("User")]
        public int OwnerId { get; set; }


        [ForeignKey("Club")]
        public int AssociatedClub { get; set; }

        public ICollection<Membership> AllowedMemberships { get; set; }


    }
}