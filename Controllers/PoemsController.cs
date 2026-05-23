using Microsoft.AspNetCore.Mvc;
using System.Linq;
using GROUP9PoetryWebsite.Data;
using GROUP9PoetryWebsite.Models;
using System;


public class PoemsController : Controller
{
    private readonly AppDbContext _context;

    public PoemsController(AppDbContext context)
    {
        _context=context;
    }

    public IActionResult Index()
    {
        var poems=_context.Poems.ToList();

        return View(poems);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Poem poem)
    {
        _context.Poems.Add(poem);

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Like(int id)
    {
        var poem = _context.Poems
            .FirstOrDefault(x=>x.Id==id);

        if(poem==null)
            return NotFound();

        poem.LikesCount++;

        _context.Likes.Add(new PoemLike
        {
            PoemId=id,
            Username="GuestUser"
        });

        _context.Notifications.Add(
            new Notification
            {
                Username=poem.Author,
                Message=
                $"Someone liked your poem '{poem.Title}'"
            });

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // GET: /Poems/Random
    [HttpGet]
    public IActionResult Random()
    {
        // 1. Fetch all poems currently saved in your Docker SQL Server database
        var allPoems = _context.Poems.ToList();

        if (!allPoems.Any())
        {
            // Fallback option just in case the seed hasn't completed yet
            return Json(new { text = "No poems washed ashore yet.", author = "System" });
        }

        // 2. Pick a random item using a random number generator
        var random = new Random();
        int randomIndex = random.Next(allPoems.Count);
        var selectedPoem = allPoems[randomIndex];

        // 3. Return the data as a pure JSON object back to your JavaScript script
        return Json(new { text = selectedPoem.Text, author = selectedPoem.Author });
    }
}