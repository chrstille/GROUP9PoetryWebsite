using System.Collections.Generic;

namespace GROUP9PoetryWebsite.Models
{
    public class Anthology
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Poem> Poems { get; set; } = new List<Poem>();
    }
}
