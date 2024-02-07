using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softeast.Lesson2.ViewModels;
using Softeast.Lesson2.WebApp.Data;
using Softeast.Lesson2.WebApp.Models;

namespace Softeast.Lesson2.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 ApplicationDbContext context,
                                 SignInManager<AppUser> signInManager
                                )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                // User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    // Password correct, login.
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }

                // Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again!";
                return View(loginViewModel);
            }

            // User not found.
            TempData["Error"] = "Wrong credentials. Please try again!";
            return View(loginViewModel);

        }
    }
}
