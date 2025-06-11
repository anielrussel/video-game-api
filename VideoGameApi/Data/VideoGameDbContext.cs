using Microsoft.EntityFrameworkCore;
using VideoGameApi.Models;

namespace VideoGameApi.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        public DbSet<VideoGameDetails> VideoGameDetails => Set<VideoGameDetails>();
        public DbSet<Developer> Developers => Set<Developer>();
        public DbSet<Publisher> Publishers => Set<Publisher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "Spider Man 2",
                    Platform = "PS5",  
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "Batman",
                    Platform = "PS5",
                }
            );
        }
    }
}
