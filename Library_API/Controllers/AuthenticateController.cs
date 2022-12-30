using Library_API.Models;
using Library_API.Models.Auth;
using Library_API.Models.DTO;
using Library_API.Repository.Interfaces;
using Library_API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
       // private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        protected Response _response;


        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ITokenRepository tokenRepository, IConfiguration configuration, IUserProfileRepository userProfileRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _userProfileRepository = userProfileRepository;
            _response = new Response();
        }

        [HttpPost]
        [Route("login")]
        //POST : /api/Authenticate/Login
        public async Task<object> Login([FromBody] LoginDTO model)
        {
            try
            {
                //Checking first if the user exists or not on the ASP .NET Core Identity User Table
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (User != null && await _userManager.CheckPasswordAsync(user, model.Password)){
                       
                        var NewGuid = Guid.NewGuid().ToString();
                        //Finding the User Profile on the Database to bring personal data like Name, username
                        var userApp = await _userProfileRepository.GetUserByUsername(model.UserName);
                        var userRoles = await _userManager.GetRolesAsync(user);

                        //Creating Auth Claims
                        
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName +"-"+ userApp.FirstName + " " + userApp.LastName),
                            new Claim(JwtRegisteredClaimNames.Jti, NewGuid),
                            new Claim(ClaimTypes.NameIdentifier, user.UserName),
                          //  new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()),
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        //Creating token for the session
                        var token = _tokenRepository.CreateToken(authClaims);
                        var refreshToken = _tokenRepository.GenerateRefreshToken();



                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Tokenizator.RefreshTokenValidityInDays);


                        await _userManager.UpdateSecurityStampAsync(user);
                        await _userManager.UpdateAsync(user);

                        APITokenDTO TokenDTO = new();

                        TokenDTO.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        TokenDTO.RefreshToken = refreshToken;
                        TokenDTO.Expiration = token.ValidTo;
                        TokenDTO.ClaimCode = user.UserName;
                        TokenDTO.ClaimName = userApp.FirstName + " " + userApp.LastName;
                        TokenDTO.NewGUID = NewGuid;
                        TokenDTO.UserRoles = userRoles;

                        _response.IsSuccess = true;
                        _response.Result = TokenDTO;
                    }
                }
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [Route("register")]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);

                if (userExists != null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "User already exists";
                    _response.ErrorMessage = new List<string>() { "User already exists" };
                }

                ApplicationUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "User creation failed! Please check user details and try again.";
                    _response.ErrorMessage = new List<string>() { result.ToString()};
                }
                else
                {
                    //CREATING ROLE FOR THE USER IF IT DOES NOT EXIST

                    if (!_roleManager.RoleExistsAsync(model.RoleName).GetAwaiter().GetResult())
                    {
                        var userRole = new IdentityRole
                        {
                            Name = model.RoleName,
                            NormalizedName = model.RoleName.ToUpper() //admin -> ADMIN
                        };
                        await _roleManager.CreateAsync(userRole);
                    }
                    await _userManager.AddToRoleAsync(user, model.RoleName);

                    _response.IsSuccess = true;
                        _response.DisplayMessage = "User created successfully!";
                        _response.ErrorMessage = new List<string>();               
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }

            return _response;
        }

        [Authorize]
        [HttpPost]
        [Route("revoke")]
        public async Task<object> Revoke([FromBody] string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    user.RefreshToken = null;
                    var result = await _userManager.UpdateAsync(user);
                    _response.IsSuccess = true;
                    _response.Result = result;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error revoking Token";
                    _response.ErrorMessage = new List<string>() { "Invalid username!" };

                }            
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }

            return _response;
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<object> RefreshToken([FromBody] TokenRefreshDTO tokenModel)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    string? accessToken = tokenModel.AccessToken;
                    string? refreshToken = tokenModel.RefreshToken;

                    var principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken);


                    if (principal != null)
                    {
                        var username = principal.Identity.Name.Split(' ');
                        var user = await _userManager.FindByNameAsync(username[0]);

                        if (user != null || user.RefreshToken == refreshToken || user.RefreshTokenExpiryTime >= DateTime.Now)
                        {
                            var newAccessToken = _tokenRepository.CreateToken(principal.Claims.ToList());
                            var newRefreshToken = _tokenRepository.GenerateRefreshToken();


                            user.RefreshToken = newRefreshToken;

                            await _userManager.UpdateAsync(user);


                            TokenRefreshDTO tokenDTO = new()
                            {
                                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                                RefreshToken = newRefreshToken
                            };


                            _response.IsSuccess = true;
                            _response.Result = tokenDTO;

                        }
                        else
                        {
                            _response.IsSuccess = false;
                            _response.DisplayMessage = "Invalid access Token!";
                            _response.ErrorMessage = new List<string>() { "Invalid access Token!" };
                        }
                    }    
                }
                else  //IF MODEL STATE IS NOT VALID
                {
                        _response.IsSuccess = false;
                        _response.DisplayMessage = "Invalid client request";
                        _response.ErrorMessage = new List<string>() { "Invalid client request" };
                    
                }                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.Message };
            }
            return _response;
        }






    }
}

