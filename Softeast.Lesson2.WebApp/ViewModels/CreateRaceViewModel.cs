using Softeast.Lesson2.WebApp.Data.Enum;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.ViewModels
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Address Address { get; set; }
        public required IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
