
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("User")]
        public int OwnerId { get; set; }

    }
}