using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;

namespace Openccpm.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Priority")]
    public class PriorityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriorityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Priority
        [HttpGet]
        public IEnumerable<Priority> GetPriority()
        {
            return _context.Priority.OrderBy( x => x.Position );
        }

        // GET: api/Priority/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriority([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var priority = await _context.Priority.SingleOrDefaultAsync(m => m.Id == id);

            if (priority == null)
            {
                return NotFound();
            }

            return Ok(priority);
        }

        // PUT: api/Priority/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriority([FromRoute] string id, [FromBody] Priority priority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priority.Id)
            {
                return BadRequest();
            }

            _context.Entry(priority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriorityExists(id))
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

        // POST: api/Priority
        [HttpPost]
        public async Task<IActionResult> PostPriority([FromBody] Priority priority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Priority.Add(priority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriority", new { id = priority.Id }, priority);
        }

        // DELETE: api/Priority/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var priority = await _context.Priority.SingleOrDefaultAsync(m => m.Id == id);
            if (priority == null)
            {
                return NotFound();
            }

            _context.Priority.Remove(priority);
            await _context.SaveChangesAsync();

            return Ok(priority);
        }

        private bool PriorityExists(string id)
        {
            return _context.Priority.Any(e => e.Id == id);
        }
    }
}