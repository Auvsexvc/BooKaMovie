using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Interfaces;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Services
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
                .Include(m=>m.Cinema)
                .Include(m=>m.Producer)
                .Include(m=>m.ActorsMovies)
                .ThenInclude(am=>am.Actor)
                .FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
