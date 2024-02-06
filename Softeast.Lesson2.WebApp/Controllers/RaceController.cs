using Microsoft.AspNetCore.Mvc;
using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Interfaces;
using Softeast.Lesson2.WebApp.Models;
using Softeast.Lesson2.WebApp.Repository;
using Softeast.Lesson2.WebApp.ViewModels;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    RaceCategory = raceVM.RaceCategory,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.Street
                    },
                    Image = result.Url.ToString()
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed!");
            }
            return View(raceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club!");
                return View("Edit", raceVM);
            }



            var raceNoTrack = await _raceRepository.GetByIdAsyncNoTracking(id);
            if (raceNoTrack == null)
            {
                return View("Error");
            }



            var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(raceVM);
            }

            if (!string.IsNullOrEmpty(raceNoTrack.Image))
            {
                _ = _photoService.DeletePhotoAsync(raceNoTrack.Image);
            }

            var race = new Race
            {
                Id = id,
                Title = raceVM.Title,
                Description = raceVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = raceVM.AddressId,
                Address = raceVM.Address
            };

            _raceRepository.Update(race);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null)
            {
                return View("Error");
            }
            return View(race);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(race.Image))
            {
                _ = _photoService.DeletePhotoAsync(race.Image);
            }

            _raceRepository.Delete(race);
            return RedirectToAction("Index");

        }
    }
}
