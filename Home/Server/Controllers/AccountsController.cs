using Home.Shared.DAL.Models;
using Home.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserService _userSerivce;

        public AccountsController( UserService userService)
        {
            _userSerivce = userService;
        }
        //private static User LoggedOutUser = new User { IsAuthenticated = false };

        //private readonly UserManager<IdentityUser> _userManager;

        //public AccountsController(UserManager<IdentityUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            List<string> errors = new List<string>();

            // is the email already used?
            var foundUser = _userSerivce.Get(model.Email);

            if(foundUser != null)
            {
                errors.Add("Email is already registered");

                return Ok(new RegisterResult { Successful = false, Errors =  errors });
            }
            else
            {
                User newUser = new User
                { 
                    UserName = model.Email,
                    EmailAddress = model.Email,
                    Password = model.Password,
                };

                _userSerivce.Create(newUser);

                return Ok(new RegisterResult { Successful = true });
            }

            // Logic goes here to look into db to see if user exists, if not add them

            //var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };

            //var result = await _userManager.CreateAsync(newUser, model.Password);

            //if (!result.Succeeded)
            //{
            //    var errors = result.Errors.Select(x => x.Description);

            //    return Ok(new RegisterResult { Successful = false, Errors = errors });

            //}
        }
    }
}

