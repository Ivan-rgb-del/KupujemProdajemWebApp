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

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task<User> LoginUser(LoginViewModel loginVM)
        {
            var appUser = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (appUser == null) return null;

            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, false);

            return result.Succeeded ? appUser : null;
        }

        public Task LogoutUser() => _signInManager.SignOutAsync();

        public async Task<bool> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if (user == null) return false;

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordViewModel.NewPassword);

            return result.Succeeded;
        }
    }
}
