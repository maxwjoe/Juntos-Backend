using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Juntos.Models
{
    public class UserDto
    {
        public int ClubId { get; set; }
        public int MembershipId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Password { get; set; }
    }
}