using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            var dropdowns = await _moviesService.GetNewMovieDropdownsVM();

            ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovieVM)
        {
            if (!ModelState.IsValid)
            {
                var dropdowns = await _moviesService.GetNewMovieDropdownsVM();

                ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "FullName");

                return View(newMovieVM);
            }
            await _moviesService.AddNewMovieAsync(newMovieVM);

            return RedirectToAction(nameof(Index));
        }
    }
}