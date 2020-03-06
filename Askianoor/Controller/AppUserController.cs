using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Askianoor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Askianoor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        //private SignInManager<AppUser> _singInManager;
        private readonly ApplicationSettings _appSettings;
        //private readonly ApplicationContext _applicationContext;

        public AppUserController(UserManager<AppUser> userManager,  IOptions<ApplicationSettings> appSettings) //, SignInManager<AppUser> signInManager , ApplicationContext applicationContext)
        {
            if(appSettings != null) {

            _userManager = userManager;
            //_singInManager = signInManager;
            _appSettings = appSettings.Value;
                //_applicationContext = applicationContext;
            }
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/AppUser/Register
        public async Task<Object> PostAppUser(ApplicationUserModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var applicationUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NickName = model.NickName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password).ConfigureAwait(true);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(applicationUser, "Users").ConfigureAwait(true);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/AppUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(model == null)
                return BadRequest(new { message = "Username or password is incorrect." });

            var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(true);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(true))
            {
                try
                {

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] { new Claim("UserID", user.Id) }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var access_token = tokenHandler.WriteToken(securityToken);

                    return Ok(new { access_token });
                }
                catch (Exception)
                {
                    throw;
                    //return BadRequest(new { message = "Funtion System Error." }); ;
                }

            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }
    }
}