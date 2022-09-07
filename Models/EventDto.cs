using Juntos.Data.Enum;

namespace Juntos.Models
{
    public class EventDto
    {
        public int OwnerId { get; set; }
        public int ClubId { get; set; }
        public int CapacityLimit { get; set; }
        public int BookingTimeLimit { get; set; }
        public RepeatOption RepeatOption { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventImageUrl { get; set; }
        public DateTime EventDateAndTime { get; set; }
    }
}