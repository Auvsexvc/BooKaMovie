using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Interfaces
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie?> GetMovieByIdAsync(int id);
    }
}
