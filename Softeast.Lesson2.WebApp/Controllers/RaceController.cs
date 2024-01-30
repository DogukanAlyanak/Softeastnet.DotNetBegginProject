using Microsoft.AspNetCore.Mvc;
using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Interfaces;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public ApplicationDbContext Context { get; }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
    }
}
