using eCommerceApp.Data.Base;
using eCommerceApp.Models;
using eCommerceApp.ViewModels;

namespace eCommerceApp.Interfaces
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie?> GetMovieByIdAsync(int id);

        Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM();

        Task AddNewMovieAsync(NewMovieVM newMovie);

        Task UpdateMovieAsync(NewMovieVM movieVM);
    }
}