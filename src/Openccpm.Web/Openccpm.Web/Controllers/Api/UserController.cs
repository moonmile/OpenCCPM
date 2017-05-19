using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Openccpm.Lib.Models;

namespace Openccpm.Web.Controllers.Api
{
    [Authorize(Roles = "ProjectMembers")]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/User
        // セキュリティ上、すべてのユーザーは取得できない。
#if false
         
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }
#endif

        // GET: api/User
        [HttpGet("Project/{id}")]
        public IEnumerable<User> GetProjectUser( string id )
        {
            var items = _context.ProjectUserView
                .Where(x => x.ProjectId == id || x.ProjectNo == id)
                .OrderBy(x => x.UserName)
                .Select(x => new User()
                {
                    Id = x.UserId,
                    UserName = x.UserName
                })
                .ToList();
            return items;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}