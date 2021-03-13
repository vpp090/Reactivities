using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(result.Succeeded)
            {
               return CreateAUserObject(user);
            }
            
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == registerDTO.Email))
            {
                return BadRequest("Email taken");
            }

            if(await _userManager.Users.AnyAsync(x => x.UserName == registerDTO.UserName))
            {
                return BadRequest("Username taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email    
            };
            
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                return CreateAUserObject(user);
            }
            return BadRequest("Problem registering user");
        }
   
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return CreateAUserObject(user);
        }


        private UserDTO CreateAUserObject(AppUser user)
        {
             return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                    UserName = user.UserName
                }; 
        }
    }
}