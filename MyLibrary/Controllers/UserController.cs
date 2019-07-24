using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyLibrary.Helpers;
using MyLibrary.Models;
using MyLibrary.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyLibrary.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        // CM
        // This constructor creates the UserController
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        // CM
        // This method returns an 201 result and the user data. 
        // GET api/user/userdata
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult> UserData()
        {
            var user = await _userManager.GetUserAsync(User);
            var userData = new UserDataResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return Ok(userData);
        }

        // CM 
        //This HttpPost handles the creation of an application user then returns the created at URL. 
        // POST api/user/register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterEntity model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    var token = AuthenticationHelper.GenerateJwtToken(model.Email,
                                                                      user,
                                                                      _configuration);
                    var rootData = new SignUpResponse(token, user.UserName, user.Email);
                    return Created("api/authentication/register", rootData);
                }
                return Ok(string.Join(",", result.Errors?.Select(error => error.Description)));
            }
            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");
        }

        // CM
        // Handles the async task of logging in for a user. Checks if the ModelState is valid awaits for the 
        // POST api/user/login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginEntity model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    var token = AuthenticationHelper.GenerateJwtToken(model.Email,
                                                                      appUser,
                                                                      _configuration);

                    var rootData = new LoginResponse(token, appUser.UserName, appUser.Email);
                    return Ok(rootData);
                }
                return StatusCode((int)HttpStatusCode.Unauthorized, "Bad Credentials");
            }
            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage ?? "Bad Request");

        }
    }
}
