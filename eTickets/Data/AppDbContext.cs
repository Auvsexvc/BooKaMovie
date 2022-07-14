using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Producer> Producers => Set<Producer>();
        public DbSet<ActorMovie> ActorsMovies => Set<ActorMovie>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<ActorMovie>().HasOne(a => a.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(a => a.ActorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}