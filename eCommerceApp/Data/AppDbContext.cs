using eCommerceApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Producer> Producers => Set<Producer>();
        public DbSet<ActorMovie> ActorsMovies => Set<ActorMovie>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActorMovie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            builder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.MovieId);
            builder.Entity<ActorMovie>().HasOne(a => a.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(a => a.ActorId);

            base.OnModelCreating(builder);
        }
    }
}