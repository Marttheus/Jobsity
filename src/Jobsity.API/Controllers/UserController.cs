using Jobsity.Domain.Models;
using Jobsity.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;

namespace Jobsity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppJwtSettings _appJwtSettings;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AppJwtSettings> appJwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appJwtSettings = appJwtSettings.Value;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CreateResponse(ModelState);

            var result = await _userManager.CreateAsync(new User { UserName = registerUser.UserName, Email = registerUser.Email }, registerUser.Password);

            var message = result.Succeeded ? "User created successfully!" : "Failed to create user, make necessary adjustments and try again.";
            return CreateResponse(result.Errors.Select(x => x.Description).ToList(), message);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CreateResponse(ModelState);

            var user = await _userManager.FindByNameAsync(loginUser.UserName);

            if (user is null) return CreateResponse(new List<string> { "User not found." });

            var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);

            if (result is not null && !result.Succeeded)
            {
                return CreateResponse(new List<string> { "Invalid password."}, null, new SignInResponse
                {
                    StatusMessage = result.ToString(),
                    AuthResponse = null,
                });
            }

            var buildUserResponse = new JwtBuilder<User>()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(user.Email)
                .WithJwtClaims()
                .BuildUserResponse();

            return CreateResponse(null, null, new SignInResponse
            {
                StatusMessage = result is null ? "Succeeded" : result.ToString(),
                AuthResponse = new AuthResponse
                {
                    AccessToken = buildUserResponse.AccessToken,
                    TokenType = "Bearer",
                    ExpiresIn = buildUserResponse.ExpiresIn,
                    UserToken = new UserToken
                    {
                        Id = buildUserResponse.UserToken.Id,
                        Email = buildUserResponse.UserToken.Email
                    }
                }
            });
        }
    }
}
