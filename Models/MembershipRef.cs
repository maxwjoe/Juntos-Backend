using Juntos.Data.Enum;

namespace Juntos.Models
{
    public class MembershipRef
    {
        public int Id { get; set; }
        public int ReferencedMembershipId { get; set; }
    }
}