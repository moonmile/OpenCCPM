using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Lib.Models;

namespace Openccpm.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Task")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public IEnumerable<TaskItem> GetTaskItem()
        {
            return _context.TaskItem;
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskItem = await _context.TaskItem.SingleOrDefaultAsync(m => m.Id == id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem([FromRoute] string id, [FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            taskItem.UpdatedAt = DateTime.Now;
            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
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

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> PostTaskItem([FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            taskItem.CreatedAt = DateTime.Now;
            _context.TaskItem.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskItem = await _context.TaskItem.SingleOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItem.Remove(taskItem);
            await _context.SaveChangesAsync();

            return Ok(taskItem);
        }

        private bool TaskItemExists(string id)
        {
            return _context.TaskItem.Any(e => e.Id == id);
        }
    }
}