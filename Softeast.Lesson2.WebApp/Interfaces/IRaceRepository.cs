using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetRacesByCity(string city);
        bool Add(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
