using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
         Club club = _context.Clubs.Include(async => async.Address).FirstOrDefault(c => c.Id == id);
            return View(club); 
        }
    }
}
