using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using FrontBack.Server.Models;

namespace FrontBack.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genre { get; set; } = default!;
    }
}
