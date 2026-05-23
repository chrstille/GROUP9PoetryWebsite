using Microsoft.AspNetCore.Mvc;
using GROUP9PoetryWebsite.Data;
using System.Linq;

public class NotificationsController : Controller
{
    private readonly AppDbContext _context;

    public NotificationsController(
        AppDbContext context)
    {
        _context=context;
    }

    public IActionResult Index()
    {
        var notifications=
            _context.Notifications.ToList();

        return View(notifications);
    }
}