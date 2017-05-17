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

namespace Openccpm.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/StartEnd")]
    public class StartEndController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StartEndController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StartEnd
        [HttpGet]
        public IEnumerable<StartEndTime> GetStartEndTime()
        {
            return _context.StartEndTime;
        }

        // GET: api/StartEnd/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStartEndTime([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var startEndTime = await _context.StartEndTime.SingleOrDefaultAsync(m => m.Id == id);

            if (startEndTime == null)
            {
                return NotFound();
            }

            return Ok(startEndTime);
        }

        /// <summary>
        /// タスクIDを指定して一覧を取得
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        // GET: api/StartEnd/Task/5
        [HttpGet("Task/{id}")]
        public IEnumerable<StartEndTime> GetStartEndTimeTask([FromRoute] string id)
        {
            return _context.StartEndTime.Where(x => x.TaskId == id).Select(x => x);
        }

        // PUT: api/StartEnd/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStartEndTime([FromRoute] string id, [FromBody] StartEndTime startEndTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != startEndTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(startEndTime).State = EntityState.Modified;
            startEndTime.UpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StartEndTimeExists(id))
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

        // POST: api/StartEnd
        [HttpPost]
        public async Task<IActionResult> PostStartEndTime([FromBody] StartEndTime startEndTime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            startEndTime.CreatedAt = DateTime.Now;
            _context.StartEndTime.Add(startEndTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStartEndTime", new { id = startEndTime.Id }, startEndTime);
        }

        // DELETE: api/StartEnd/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStartEndTime([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var startEndTime = await _context.StartEndTime.SingleOrDefaultAsync(m => m.Id == id);
            if (startEndTime == null)
            {
                return NotFound();
            }

            _context.StartEndTime.Remove(startEndTime);
            await _context.SaveChangesAsync();

            return Ok(startEndTime);
        }

        private bool StartEndTimeExists(string id)
        {
            return _context.StartEndTime.Any(e => e.Id == id);
        }
    }
}