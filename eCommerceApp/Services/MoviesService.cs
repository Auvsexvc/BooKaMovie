using eCommerceApp.Data;
using eCommerceApp.Data.Base;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using eCommerceApp.ViewModels;
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

        public override async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var data = await _dbContext
                .Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.ActorsMovies)
                .ThenInclude(am => am.Actor)
                .ToListAsync();

            return data;
        }

        public async Task AddNewMovieAsync(NewMovieVM newMovie)
        {
            var newMov = new Movie()
            {
                Name = newMovie.Name,
                Description = newMovie.Description,
                Price = newMovie.Price,
                StartDate = newMovie.StartDate,
                EndDate = newMovie.EndDate,
                MovieCategory = newMovie.MovieCategory,
                CinemaId = newMovie.CinemaId,
                ProducerId = newMovie.ProducerId,
                ImageURL = newMovie.ImageURL
            };

            await _dbContext.AddAsync(newMov);
            await _dbContext.SaveChangesAsync();

            foreach (var actorId in newMovie.ActorIds)
            {
                ActorMovie newActorMovie = new()
                {
                    MovieId = newMov.Id,
                    ActorId = actorId
                };
                await _dbContext.AddAsync(newActorMovie);
            }
            await _dbContext.SaveChangesAsync();
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

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM()
        {
            return new NewMovieDropdownsVM()
            {
                Actors = await _dbContext.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Producers = await _dbContext.Producers.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _dbContext.Cinemas.OrderBy(a => a.Name).ToListAsync()
            };
        }

        public async Task UpdateMovieAsync(NewMovieVM movieVM)
        {
            var movie = await _dbContext.Movies.FirstAsync(m => m.Id == movieVM.Id);

            if (movie != null)
            {
                movie.Name = movieVM.Name;
                movie.Description = movieVM.Description;
                movie.Price = movieVM.Price;
                movie.StartDate = movieVM.StartDate;
                movie.EndDate = movieVM.EndDate;
                movie.MovieCategory = movieVM.MovieCategory;
                movie.CinemaId = movieVM.CinemaId;
                movie.ProducerId = movieVM.ProducerId;
                movie.ImageURL = movieVM.ImageURL;

                var movieActors = _dbContext.ActorsMovies.Where(am => am.MovieId == movieVM.Id).ToList();
                _dbContext.ActorsMovies.RemoveRange(movieActors);

                foreach (var actorId in movieVM.ActorIds)
                {
                    ActorMovie newActorMovie = new()
                    {
                        MovieId = movieVM.Id,
                        ActorId = actorId
                    };
                    await _dbContext.AddAsync(newActorMovie);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}