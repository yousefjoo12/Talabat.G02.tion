using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.DTOs;
using Talabat.APIS.Erorrs;
using Talabat.APIS.Extensions;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
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
                Token = await _authService.CreateTokenAsync(user, _userManager)
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
            var result = await _userManager.CreateAsync(user, model.Password);
            // return Ok(UserDTO)
            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentsUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]
        public  async Task<ActionResult<AddressIDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            return Ok(_mapper.Map<AddressI,AddressIDto>(user.Address));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        [HttpPut("address")]
        public async Task<ActionResult<AddressI>> UpdateUserAddress(AddressIDto address)
        {
            var updateaddress =  _mapper.Map<AddressIDto, AddressI>(address);
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            updateaddress.Id=user.Address.Id;

            user.Address = updateaddress;

            var result =await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(address);
           

        }
    }
}
