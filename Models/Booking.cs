using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juntos.Models
{
    public class Booking
    {

        // --- Intrinsic Properties ---

        public int Id { get; set; }
        public bool isWaitlisted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // --- Relationships ---

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }


    }
}