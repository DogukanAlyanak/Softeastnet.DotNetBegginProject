using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
    }
}
