using Softeast.Lesson2.WebApp.Data.Enum;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.ViewModels
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
