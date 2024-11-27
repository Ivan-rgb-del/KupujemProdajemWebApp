using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
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
        private readonly AccountService _accountService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext, ITokenService tokenService, AccountService accountService)
        {
            _context = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountService = accountService;
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

            try
            {
                await _accountService.RegisterNewUser(registerVM);
                return RedirectToAction("Index", "Advertisement");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(registerVM);
            }
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _accountService.LoginUser(loginVM);
            if (user != null)
            {
                return Redirect("/");
            }
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUser();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ResetPassword()
        {
            var resetPasswordViewModel = new ResetPasswordViewModel();
            return View(resetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var result = await _accountService.ResetPassword(resetPasswordViewModel);

            if (result)
            {
                return RedirectToAction("Login", "Account");
            }

            TempData["Error"] = "Password reset failed!";
            return View(resetPasswordViewModel);
        }
    }
}
