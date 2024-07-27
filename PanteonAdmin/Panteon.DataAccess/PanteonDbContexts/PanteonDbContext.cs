using Microsoft.EntityFrameworkCore;
using Panteon.Entities.Entities;

namespace Panteon.DataAcces.PanteonDbContexts
{
    public class PanteonDbContext : DbContext
    {
        public PanteonDbContext(DbContextOptions<PanteonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraint for Username and Email
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName, u.Email })
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
    }
}
