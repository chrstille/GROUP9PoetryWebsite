using GROUP9PoetryWebsite.Data;
using GROUP9PoetryWebsite.Models;
using Microsoft.AspNetCore.Authentication; // Required for SignOutAsync
using Microsoft.AspNetCore.Authentication.Cookies; // Required for CookieAuthenticationDefaults
using Microsoft.AspNetCore.Authorization; // Required for async methods
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for async database operations
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var username = User.Identity?.Name;

            // Get all poems
            var poems = _context.Poems
                .Include(p => p.Anthology)
                .OrderByDescending(p => p.Id).ToList();

            // Store IDs of poems liked by the current user
            ViewBag.LikedPoemIds = new HashSet<int>();
            if (username != null)
            {
                ViewBag.LikedPoemIds = _context.Likes
                    .Where(l => l.Username == username)
                    .Select(l => l.PoemId)
                    .ToHashSet();
            }

            ViewBag.PoemCount = poems.Count;
            return View(poems);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLike(int poemId)
        {
            var username = User.Identity?.Name;
            if (username == null) return Unauthorized();

            // Check if the user already liked this poem
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PoemId == poemId && l.Username == username);

            var poem = await _context.Poems.FindAsync(poemId);
            if (poem == null) return NotFound();

            bool isNowLiked;

            if (existingLike != null)
            {
                // Remove like
                _context.Likes.Remove(existingLike);
                poem.LikesCount = Math.Max(0, poem.LikesCount - 1);
                isNowLiked = false;
            }
            else
            {
                // Add like
                _context.Likes.Add(new PoemLike { PoemId = poemId, Username = username });
                poem.LikesCount++;
                isNowLiked = true;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, liked = isNowLiked, newCount = poem.LikesCount });
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

            // 1 is default for no anthology picked
            poem.Anthology = _context.Anthologies.FirstOrDefault(a => a.Id == 1);
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