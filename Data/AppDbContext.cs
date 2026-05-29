using Microsoft.EntityFrameworkCore;
using GROUP9PoetryWebsite.Models;

namespace GROUP9PoetryWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Poem> Poems { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<PoemLike> Likes { get; set; } = null!;
        public DbSet<Anthology> Anthologies { get; set; } = null!;
    }
}