using eCommerceApp.Data;
using eCommerceApp.Data.Base;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _dbContext;

        public MoviesService(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _dbContext
                .Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.ActorsMovies)
                .ThenInclude(am => am.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}