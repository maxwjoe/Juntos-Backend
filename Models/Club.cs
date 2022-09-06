
using System.ComponentModel.DataAnnotations;

namespace Juntos.Models
{
    public class Club
    {

        // --- Intrinsic Properties ---

        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string ClubImageURL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // --- Relationships --- 

        public User Owner { get; set; }

        public ICollection<User> Members { get; set; }


    }
}