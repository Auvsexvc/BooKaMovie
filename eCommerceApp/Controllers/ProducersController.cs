using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _producersService;

        public ProducersController(IProducersService producersService)
        {
            _producersService = producersService;
        }

        public async Task<IActionResult> Index() => View(await _producersService.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _producersService.AddAsync(producer);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id) => await GetProducerAsync(id);

        public async Task<IActionResult> Edit(int id) => await GetProducerAsync(id);

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            if (id == producer.Id)
            {
                await _producersService.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        public async Task<IActionResult> Delete(int id) => await GetProducerAsync(id);

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);

            if (producer == null)
            {
                return View("NotFound");
            }
            await _producersService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> GetProducerAsync(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);

            if (producerDetails == null)
            {
                return View("NotFound");
            }

            return View(producerDetails);
        }
    }
}