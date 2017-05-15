using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Openccpm.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Openccpm.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _externalCookieScheme;

        public UserAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
        }

        // POST: /Account/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<User> Login([FromBody]LoginParameter login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.LoginId, login.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByEmailAsync(login.LoginId);
                
                var user = new User()
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    Email = appUser.Email
                };
                return user;
            }
            else
            {
                var user = new User() { Id = "" };
                return user;
            }
        }
        // GET: /Account/Logout
        [HttpGet("Logout")]
        [AllowAnonymous]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
