using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext Context { get; }

        public IActionResult Index()
        {
            List<Race> races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            Race race = _context.Races.Include(async => async.Address).FirstOrDefault(c => c.Id == id);
            return View(race);
        }
    }
}
