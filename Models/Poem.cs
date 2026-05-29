using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GROUP9PoetryWebsite.Models;

public class Poem
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Text { get; set; } = "";

    public string Author { get; set; } = "";

    public int LikesCount { get; set; }
    public int AnthologyId { get; set; }

    public List<PoemLike> Likes { get; set; } = new();

    public Anthology Anthology { get; set; }

}