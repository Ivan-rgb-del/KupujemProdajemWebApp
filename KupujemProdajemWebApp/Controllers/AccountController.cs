using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext, ITokenService tokenService)
        {
            _context = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use!";
                return View(registerVM);
            }

            var newUser = new User()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                Address = new Address
                {
                    City = registerVM.Address.City,
                    Street = registerVM.Address.Street,
                },
            };

            var createdUser = await _userManager.CreateAsync(newUser, registerVM.Password);

            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");

            if (roleResult.Succeeded)
            {
                new NewUserViewModel
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    Token = _tokenService.CreateToken(newUser)
                };
            }

            return RedirectToAction("Index", "Advertisement");
        }
    }
}
