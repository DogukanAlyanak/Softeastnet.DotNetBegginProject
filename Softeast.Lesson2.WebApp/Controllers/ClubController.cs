using Microsoft.AspNetCore.Mvc;
using Softeast.Lesson2.WebApp.Interfaces;
using Softeast.Lesson2.WebApp.Models;
using Softeast.Lesson2.WebApp.ViewModels;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.Street
                    },
                    Image = result.Url.ToString()
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("", "Photo upload failed!");
            }
            return View(clubVM);
        }
    }
}
