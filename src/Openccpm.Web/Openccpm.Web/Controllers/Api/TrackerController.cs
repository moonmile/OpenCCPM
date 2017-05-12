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
    [Route("api/Tracker")]
    public class TrackerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrackerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tracker
        [HttpGet]
        public IEnumerable<Tracker> GetTracker()
        {
            return _context.Tracker.OrderBy(x => x.Position );
        }

        // GET: api/Tracker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTracker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tracker = await _context.Tracker.SingleOrDefaultAsync(m => m.Id == id);

            if (tracker == null)
            {
                return NotFound();
            }

            return Ok(tracker);
        }

        // PUT: api/Tracker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTracker([FromRoute] string id, [FromBody] Tracker tracker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tracker.Id)
            {
                return BadRequest();
            }

            _context.Entry(tracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackerExists(id))
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

        // POST: api/Tracker
        [HttpPost]
        public async Task<IActionResult> PostTracker([FromBody] Tracker tracker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tracker.Add(tracker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTracker", new { id = tracker.Id }, tracker);
        }

        // DELETE: api/Tracker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTracker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tracker = await _context.Tracker.SingleOrDefaultAsync(m => m.Id == id);
            if (tracker == null)
            {
                return NotFound();
            }

            _context.Tracker.Remove(tracker);
            await _context.SaveChangesAsync();

            return Ok(tracker);
        }

        private bool TrackerExists(string id)
        {
            return _context.Tracker.Any(e => e.Id == id);
        }
    }
}