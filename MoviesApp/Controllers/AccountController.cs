using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.JwtToken;
using MoviesApp.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtTokenProvider _jwtToken;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IJwtTokenProvider jwtTokenProvider
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtToken = jwtTokenProvider;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            IdentityUser user =null;
            if (loginModel.UserName != null)
            {
             user = await _userManager.FindByNameAsync(loginModel.UserName);
            }
            else if (loginModel.Email != null)
            {
                user = await _userManager.FindByEmailAsync(loginModel.Email);
            }
            if (user==null)
            {
                return Problem("login failed, username or password are incorrect");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            if (!result.Succeeded)
            {
                
                return Problem("login failed, username or password are incorrect");
            }
            else
            {
                return Ok(new
                {
                    result = result,
                    username = loginModel.Email,
                    email = loginModel.Email,
                    token = _jwtToken.GenerateToken(user, null, new List<Claim>())
                });
            }

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
            };
            var result  = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
               return Ok(new 
                {
                    result
                });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Problem(string.Join(",",errors));
            }
        }
    }
}
