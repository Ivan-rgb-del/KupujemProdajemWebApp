using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KupujemProdajemWebApp.Services
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> RegisterNewUser(RegisterViewModel registerVM)
        {
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                throw new Exception("This email address is already in use!");
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

            var result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }

            return result;
        }
    }
}
