using GROUP9PoetryWebsite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GROUP9PoetryWebsite.ViewModels
{
    public class PoemViewModel
    {
        public IEnumerable<Poem> Poems { get; set; } // For your feed
        public Poem Poem { get; set; }               // For the form
        public IEnumerable<SelectListItem> AnthologyOptions { get; set; }

        // Add this to capture a user-typed title if they aren't using the dropdown
       
        public string NewAnthologyTitle { get; set; }
    }
}
