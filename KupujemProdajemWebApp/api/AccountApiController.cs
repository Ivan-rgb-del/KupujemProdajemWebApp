using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountApiController(AccountService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerVM)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingUser = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
                if (existingUser != null)
                {
                    return BadRequest("User with this email address already exists.");
                }

                var appUser = new User
                {
                    UserName = registerVM.EmailAddress,
                    Email = registerVM.EmailAddress,
                    Address = new Address
                    {
                        City = registerVM.Address.City,
                        Street = registerVM.Address.Street,
                    }
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerVM.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("User created!");
                    } else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                } else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
