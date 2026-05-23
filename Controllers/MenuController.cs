using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication; // Required for SignOutAsync
using Microsoft.AspNetCore.Authentication.Cookies; // Required for CookieAuthenticationDefaults
using GROUP9PoetryWebsite.Data;
using GROUP9PoetryWebsite.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Required for async methods

namespace GROUP9PoetryWebsite.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var poems = _context.Poems.ToList();
            return View(poems);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Poem poem)
        {
            // LOGGING: Check if the data arrived
            if (string.IsNullOrEmpty(poem.Title) || string.IsNullOrEmpty(poem.Text))
            {
                // If this hits, the names in your HTML <input name="..."> do not match the class properties
                return BadRequest("Title or Text is empty!");
            }

            poem.Author = User.Identity?.Name ?? "Anonymous";
            
            _context.Poems.Add(poem);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}