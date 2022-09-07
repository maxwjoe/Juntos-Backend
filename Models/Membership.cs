using Juntos.Data.Enum;

namespace Juntos.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public RepeatOption BillingFrequency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}