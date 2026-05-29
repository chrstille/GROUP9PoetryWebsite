using GROUP9PoetryWebsite.Data;
using GROUP9PoetryWebsite.Models;
using GROUP9PoetryWebsite.ViewModels;
using Microsoft.AspNetCore.Authentication; // Required for SignOutAsync
using Microsoft.AspNetCore.Authentication.Cookies; // Required for CookieAuthenticationDefaults
using Microsoft.AspNetCore.Authorization; // Required for async methods
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            // 1. Get all poems
            var poems = _context.Poems
                .Include(p => p.Anthology)
                .OrderByDescending(p => p.Id).ToList();

            // 2. Store IDs of poems liked by the current user (Keep these for your logic)
            ViewBag.LikedPoemIds = new HashSet<int>();
            if (username != null)
            {
                ViewBag.LikedPoemIds = _context.Likes
                    .Where(l => l.Username == username)
                    .Select(l => l.PoemId)
                    .ToHashSet();
            }

            // 3. Create the ViewModel
            var viewModel = new PoemViewModel
            {
                Poems = poems,
                Poem = new Poem(), // Initializes the empty object for the "Create" form
                AnthologyOptions = _context.Anthologies
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Title
                    }).ToList()
            };

            ViewBag.PoemCount = poems.Count;
            return View(viewModel); // Return the ViewModel
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
        public IActionResult Create(PoemViewModel vm, string newAnthologyTitle)
        {

            // Check if the anthology title already exists in the table
            if (!string.IsNullOrWhiteSpace(newAnthologyTitle))
            {
                var existingAnthology = _context.Anthologies
                    .FirstOrDefault(a => a.Title.Trim().ToLower() == newAnthologyTitle.Trim().ToLower());

                if (existingAnthology != null)
                {
                    // If it exists, use the existing one
                    vm.Poem.AnthologyId = existingAnthology.Id;
                }
                else
                {
                    // If it doesn't exist, add a new record of anthology
                    var newAnthology = new Anthology
                    {
                        Title = newAnthologyTitle.Trim(),
                        Description = "A collection of verses." // Default description
                    };
                    _context.Anthologies.Add(newAnthology);
                    _context.SaveChanges(); // Save first to get the generated ID
                    vm.Poem.AnthologyId = newAnthology.Id;
                }
            }
            else if (vm.Poem.AnthologyId == 0)
            {
                // Fallback to default if nothing selected/entered
                vm.Poem.AnthologyId = 1;
            }

            // Proceed with Poem creation
            var poem = vm.Poem;
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