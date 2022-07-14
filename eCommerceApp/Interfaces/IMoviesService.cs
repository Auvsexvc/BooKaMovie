using eCommerceApp.Data.Base;
using eCommerceApp.Models;

namespace eCommerceApp.Interfaces
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie?> GetMovieByIdAsync(int id);
    }
}