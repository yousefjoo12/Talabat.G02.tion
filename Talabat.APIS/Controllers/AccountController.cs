using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Erorrs;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIS.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")] // post  api/Account/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Passwoed, false);
            if (result.Succeeded is false) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This Will Be Token"
            });
        }

        [HttpPost("register")] // post  api/Account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            // create user
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber
            };
            // creatAsync
            var result =await _userManager.CreateAsync(user, model.Password);
            // return Ok(UserDTO)
            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This Will Be Token"
            });
            
        }
    }
}
