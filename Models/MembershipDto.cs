using Juntos.Data.Enum;

namespace Juntos.Models
{
    public class MembershipDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public RepeatOptions BillingOption { get; set; }
        public int AssociatedClub { get; set; }
    }
}