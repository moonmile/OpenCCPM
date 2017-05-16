using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;
using Openccpm.Lib.Models;

namespace Openccpm.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public IEnumerable<Status> GetStatus()
        {
            return _context.Status.OrderBy( x => x.Position );
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _context.Status.SingleOrDefaultAsync(m => m.Id == id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus([FromRoute] string id, [FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Status
        [HttpPost]
        public async Task<IActionResult> PostStatus([FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _context.Status.SingleOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();

            return Ok(status);
        }

        private bool StatusExists(string id)
        {
            return _context.Status.Any(e => e.Id == id);
        }
    }
}