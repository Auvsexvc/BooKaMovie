﻿using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        public async Task<IActionResult> Index() => View(await _cinemasService.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Logo,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _cinemasService.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) => await GetCinemaAsync(id);

        public async Task<IActionResult> Edit(int id) => await GetCinemaAsync(id);

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            if (id == cinema.Id)
            {
                await _cinemasService.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Delete(int id) => await GetCinemaAsync(id);

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);

            if (cinema == null)
            {
                return View("NotFound");
            }
            await _cinemasService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> GetCinemaAsync(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("NotFound");
            }

            return View(cinemaDetails);
        }
    }
}