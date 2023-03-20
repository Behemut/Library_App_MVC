using Domain;
using IdentityServer4.Models;
using Library_API.AppUserAccesor;
using Library_API.Models.Auth;
using Library_API.Models.DTO;
using Library_API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        public readonly UserManager<UsersPerson> _userManager;
        public readonly TokenGenerator _tokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserAccesor _userAccesor;
        

        public AuthenticateController(UserManager<UsersPerson> userManager,
                                       TokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
        }




        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserAppDto>> Register(RegisterDTO registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                ModelState.AddModelError("Username", "Username is taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                ModelState.AddModelError("email", "Email is taken");
                return ValidationProblem();
            }

            //If everything went ok
            var user = new UsersPerson
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //Creating the Role if doesnt exists in the current database
            if (!_roleManager.RoleExistsAsync(registerDto.RolName).GetAwaiter().GetResult())
            {
                var userRole = new IdentityRole
                {
                    Name = registerDto.RolName,
                    NormalizedName = registerDto.RolName.ToUpper() //admin -> ADMIN
                };
                await _roleManager.CreateAsync(userRole);
            }
            await _userManager.AddToRoleAsync(user, registerDto.RolName);

            if (result.Succeeded)
            {
                await SetRefreshToken(user);
                return CreateUserObject(user);
            }
            return BadRequest(result.Errors);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserAppDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                        .SingleOrDefaultAsync(x => x.UserName == loginDto.Username && x.Active == true );

            if (user == null) return Unauthorized("Invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid password");


            await SetRefreshToken(user);

            return CreateUserObject(user);

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserAppDto>> GetCurrentUser()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name));
            await SetRefreshToken(user);
            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserAppDto>> RefreshToken()
        {
            var user = await _userManager.Users
                        .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name));
            if (user == null) return Unauthorized();
            //var oldToken = user.SingleOrDefault(x => x.Token == user.Token);
            var oldToken = _userManager.Users.SingleOrDefault(x => x.Token == user.Token);
            if (oldToken != null && !oldToken.IsActive) return Unauthorized();
            return CreateUserObject(user);
        } 



        private async Task SetRefreshToken(UsersPerson user)
        {
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            //user.RefreshTokens.Add(refreshToken);
            user.Token = refreshToken.Token; 
            await _userManager.UpdateAsync(user);
          
        }
            
        private UserAppDto CreateUserObject(UsersPerson user)
        {
            var username =  _userManager.FindByIdAsync(user.Id);
            return new UserAppDto
            {
                DisplayName = user.FirstName + " " + user.LastName,
                Token = _tokenGenerator.CreateToken(user),
                RefreshToken = user.Token,
                Username = user.UserName,
                Role = _userManager.GetRolesAsync(user).Result[0],
            };
        }

        
    }
}

