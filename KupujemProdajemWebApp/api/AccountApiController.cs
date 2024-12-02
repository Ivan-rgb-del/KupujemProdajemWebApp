using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public AccountApiController(AccountService accountService, UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
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
                        return Ok(
                            new NewUserViewModel
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginVM)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
                if (user == null) return Unauthorized("Invalid Email or Password!");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, false);
                if (!result.Succeeded) return Unauthorized("Invalid Email or Password!");

                return Ok(
                    new NewUserViewModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            } catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
