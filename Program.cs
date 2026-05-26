using GROUP9PoetryWebsite.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ── Database initialisation ──────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    try
    {
        Console.WriteLine(">>> [DB] Checking connection...");
        bool canConnect = context.Database.CanConnect();
        Console.WriteLine($">>> [DB] CanConnect = {canConnect}");

        Console.WriteLine(">>> [DB] Running Migrate()...");
        context.Database.Migrate();
        Console.WriteLine(">>> [DB] Migrate() complete.");

        Console.WriteLine(">>> [DB] Running SeedData...");
        SeedData.Initialize(context);
        Console.WriteLine(">>> [DB] SeedData complete.");

        int count = context.Poems.Count();
        Console.WriteLine($">>> [DB] Poems in database after seed: {count}");
    }
    catch (Exception ex)
    {
        // Print the FULL exception so nothing is hidden
        Console.WriteLine(">>> [DB] EXCEPTION during DB init:");
        Console.WriteLine(ex.ToString());

        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialization.");
    }
}
// ────────────────────────────────────────────────────────────────────────────

app.Run();