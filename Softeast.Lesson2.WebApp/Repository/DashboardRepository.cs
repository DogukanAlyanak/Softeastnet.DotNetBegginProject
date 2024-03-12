using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Interfaces;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContetAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContetAccessor)
        {
            _context = context;
            _httpContetAccessor = httpContetAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContetAccessor.HttpContext?.User;
            var userClubs = _context.Clubs.Where(e => e.AppUser.Id == curUser.ToString());
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContetAccessor.HttpContext?.User;
            var userRaces = _context.Races.Where(e => e.AppUser.Id == curUser.ToString());
            return userRaces.ToList();
        }
    }
}
