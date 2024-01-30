using Microsoft.AspNetCore.Mvc;
using Softeast.Lesson2.WebApp.Interfaces;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club); 
        }

        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Club club)
        {
            if (!ModelState.IsValid)
            {
                return View(club);
            }
            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
    }
}
