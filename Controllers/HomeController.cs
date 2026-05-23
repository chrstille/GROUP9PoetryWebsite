using Microsoft.AspNetCore.Mvc;
using GROUP9PoetryWebsite.Data; 
using System.Linq;

namespace GROUP9PoetryWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // The constructor "injects" the database connection
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
       public IActionResult Index()
        {
            if (_context == null) 
            {
                return Content("CRITICAL ERROR: _context is null. Check your Dependency Injection in Program.cs!");
            }
            
            var poems = _context.Poems.ToList(); 
            return View(poems);
        }
    }
}