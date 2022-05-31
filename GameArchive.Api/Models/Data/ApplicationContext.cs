using Microsoft.EntityFrameworkCore;

namespace GameArchive.Api.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Navigation(g => g.Genres).AutoInclude();
        }
    }
}
