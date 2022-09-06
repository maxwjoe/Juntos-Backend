using Juntos.Data.Enum;

namespace Juntos.Models
{
    public class EventDto
    {
        public int OwnerId { get; set; }
        public int AssociatedClub { get; set; }
        public int[] AllowedMembershipIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool DoesRepeat { get; set; }
        public int CapacityLimit { get; set; }
        public int BookingTimeLimitMinutes { get; set; }
        public RepeatOptions RepeatOption { get; set; }
        public DateTime EventDateAndTime { get; set; }
    }
}