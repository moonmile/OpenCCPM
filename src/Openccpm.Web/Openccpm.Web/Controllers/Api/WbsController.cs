using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;

namespace Openccpm.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/WBS")]
    public class WbsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WbsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WBS
        [HttpGet]
        public IEnumerable<WbsView> GetTicket()
        {
            var items = _context.WbsView.Select(x => x).ToList();
            return items;
        }

        // GET: api/WBS/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.WbsView.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }


    [Route("api/ProjectItem")]
    public class ProjectItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WBS
        [HttpGet]
        public IEnumerable<ProjectItem> Get()
        {
            var items = _context.ProjectItem.Select(x => x).ToList();
            return items;
        }

        // GET: api/WBS/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.ProjectItem.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }

    [Route("api/ProjectView")]
    public class ProjectViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WBS
        [HttpGet]
        public IEnumerable<ProjectView> Get()
        {
            var items = _context.ProjectView.Select(x => x).ToList();
            return items;
        }

        // GET: api/WBS/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.ProjectView.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}