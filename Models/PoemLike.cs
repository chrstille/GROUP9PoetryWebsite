using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GROUP9PoetryWebsite.Models
{
    public class PoemLike
    {
        [Key]
        public int Id { get; set; } // Unique ID for this specific like record

        [ForeignKey("Poem")]
        public int PoemId { get; set; } // The ID of the poem being liked
        
        public Poem? Poem { get; set; } // Navigation property

        [Required]
        public string Username { get; set; } = "";
    }
}