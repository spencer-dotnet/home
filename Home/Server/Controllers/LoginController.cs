using Home.Shared;
using Home.Shared.DAL.Models;
using Home.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Home.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserService _userService;

        public LoginController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {

            var user = _userService.Get(login.Email);

            // check if user exists
            if (user == null)
            {
                return Ok(new LoginResult { Successful = false, Error = "Email/Password is not correct" });
            }

            if (!PasswordHash.VerifyPassword(login.Password, user.Password))
            {
                return Ok(new LoginResult { Successful = false, Error = "Email/Password is not correct" });
            }

            //var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            //if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });


            var claims = new[]
            {
            new Claim(ClaimTypes.Name, login.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        //    private string GenerateJSONWebToken(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.Role, user.Role)
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Issuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(20),
        //        signingCredentials: credentials);

        //    var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

        //    return encodeToken;
        //}
    }
}
