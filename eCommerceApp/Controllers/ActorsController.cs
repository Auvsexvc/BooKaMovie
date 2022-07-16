using eCommerceApp.Data.Static;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;

        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index() => View(await _actorsService.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorsService.AddAsync(actor);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id) => await GetDetailAsync(id);

        public async Task<IActionResult> Edit(int id) => await GetDetailAsync(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            if (id == actor.Id)
            {
                await _actorsService.UpdateAsync(id, actor);
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        public async Task<IActionResult> Delete(int id) => await GetDetailAsync(id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }
            await _actorsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> GetDetailAsync(int id)
        {
            if (Request.Headers["Referer"] != string.Empty)
            {
                ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }

            var actorDetails = await _actorsService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }
    }
}