using System.ComponentModel.DataAnnotations;

namespace GROUP9PoetryWebsite.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = "";

        public string Message { get; set; } = "";

        public bool IsRead { get; set; }
    }
}