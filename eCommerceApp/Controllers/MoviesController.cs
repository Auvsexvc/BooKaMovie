using eCommerceApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _moviesService.GetAllAsync(m => m.Cinema);

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _moviesService.GetMovieByIdAsync(id);

            return View(movieDetail);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}