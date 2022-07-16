using eCommerceApp.Data.Static;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index() => View(await _moviesService.GetAllAsync(m => m.Cinema));

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id) => View(await _moviesService.GetMovieByIdAsync(id));

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

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _moviesService.GetMovieByIdAsync(id);

            if (movieDetails == null)
            {
                return View("NotFound");
            }

            var movieVM = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.ActorsMovies.ConvertAll(am => am.ActorId)
            };

            var dropdowns = await _moviesService.GetNewMovieDropdownsVM();

            ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "FullName");

            return View(movieVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movieVM)
        {
            if (id != movieVM.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var dropdowns = await _moviesService.GetNewMovieDropdownsVM();

                ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "FullName");

                return View(movieVM);
            }
            await _moviesService.UpdateMovieAsync(movieVM);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var movies = await _moviesService.GetAllAsync(m => m.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = movies.Where(m => m.Name.ToLower().Contains(searchString.ToLower()) || m.Description.ToLower().Contains(searchString.ToLower())).ToList();
                ViewBag.SearchString = searchString;

                return View("Index", filteredResult);
            }

            return View("Index", movies);
        }
    }
}