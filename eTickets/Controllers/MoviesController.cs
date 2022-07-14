using eTickets.Data;
using eTickets.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace eTickets.Controllers
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
            var data = await _moviesService.GetAllAsync(m=>m.Cinema);

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _moviesService.GetMovieByIdAsync(id);

            return View(movieDetail);
        }
    }
}
