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

namespace Openccpm.Web.Controllers
{
    [Authorize(Roles = "ProjectMembers")]
    [Produces("application/json")]
    [Route("api/Project")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/Project
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Project> GetTicket()
        {
            return _context.Project
                .Where( x => x.Deleted == false )
                .OrderByDescending( x => x.CreatedAt );
        }

        // GET: api/Project/5
        [Authorize(Roles = "ProjectMembers")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 自分の所属するプロジェクトだけを表示する
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var cnt = _context.ProjectUserView
                .Where( x => x.ProjectId == id || x.ProjectNo == id ) 
                .Where( x => x.UserId == userId)
                .Count();
            if (cnt == 0)
            {
                return BadRequest();
            }

            var item = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // PUT: api/Project/5
        [Authorize(Roles = "ProjectAdministrators")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] string id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.Id)
            {
                return BadRequest();
            }

            project.UpdatedAt = DateTime.Now;
            _context.Update(project);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Project
        [Authorize(Roles = "ProjectAdministrators")]
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project.CreatedAt = DateTime.Now;
            _context.Add(project);
            await _context.SaveChangesAsync();
            // 自分をプロジェクトに追加する
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var projUser = new ProjectUser() { ProjectId = project.Id, UserId = userId };
            _context.Add(projUser);
            await _context.SaveChangesAsync();

            return await GetTicket(project.Id);
        }

        // DELETE: api/Task/5
        [Authorize(Roles = "ProjectAdministrators")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            // 削除フラグのみ
            item.Deleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        private bool itemExists(string id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}