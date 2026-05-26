using GROUP9PoetryWebsite.Data;
using GROUP9PoetryWebsite.Models;
using Microsoft.AspNetCore.Authentication; // Required for SignOutAsync
using Microsoft.AspNetCore.Authentication.Cookies; // Required for CookieAuthenticationDefaults
using Microsoft.AspNetCore.Authorization; // Required for async methods
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GROUP9PoetryWebsite.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;

            // Quick Debug: Test connection manually
            if (!_context.Database.CanConnect())
            {
                throw new Exception("Cannot connect to the database. Check your connection string in appsettings.json.");
            }
        }

        public IActionResult Index()
        {
            var count = _context.Poems.Count();
            var poems = _context.Poems.ToList();

            ViewBag.PoemCount = count;

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