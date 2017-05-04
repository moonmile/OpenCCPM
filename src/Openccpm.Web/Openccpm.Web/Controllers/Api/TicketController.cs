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
    [Route("api/Ticket")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public IEnumerable<Ticket> GetTicket()
        {
            // TaskItem ‚Æ TicketItem ‚ðƒŠƒ“ƒN‚·‚é
            var q = from ta in _context.TaskItem
                    join ti in _context.TicketItem on ta.Id equals ti.TaskId
                    select new { task = ta, ticket = ti };
            var items = new List<Ticket>();
            foreach ( var it in q.ToList() )
            {
                items.Add(new Ticket(it.task, it.ticket));
            }
            return items;
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var q = from ta in _context.TaskItem
                    join ti in _context.TicketItem on ta.Id equals ti.TaskId
                    where ta.Id == id
                    select new { task = ta, ticket = ti };
            var it = await q.SingleOrDefaultAsync();
            var item = new Ticket(it.task, it.ticket);

            if (item == null)
            {
                return NotFound();
            }

            /*
            // ƒŠƒŒ[ƒVƒ‡ƒ“•ª‚àŽæ“¾‚µ‚Ä‚¨‚­
            item.PlanStartEnds = _context.StartEndTime.Where(x => x.TaskId == id && x.IsPlan == true ).Select(x => x).ToList();
            item.DoneStartEnds = _context.StartEndTime.Where(x => x.TaskId == id && x.IsPlan == false).Select(x => x).ToList();
            item.ParentTask = (
                from m in _context.TaskItem
                join s in _context.TaskTree.Where(x => x.ChildTaskId == id) on m.Id equals s.ParentTaskId
                select m
                ).FirstOrDefault();
            item.ChildTasks = (
                from m in _context.TaskItem
                join s in _context.TaskTree.Where(x => x.ParentTaskId == id) on m.Id equals s.ChildTaskId
                select m
                ).ToList();
            item.Tracker = await _context.Tracker.SingleOrDefaultAsync(m => m.Id == item.Tracker.Id);
            item.Status = await _context.Status.SingleOrDefaultAsync(m => m.Id == item.Status.Id);
            item.Priority = await _context.Priority.SingleOrDefaultAsync(m => m.Id == item.Priority.Id);
            item.AssignedTo = await _context.User.SingleOrDefaultAsync(m => m.Id == item.AssignedTo.Id);
            item.Author = await _context.User.SingleOrDefaultAsync(m => m.Id == item.Author.Id);
            */
            return Ok(item);
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] string id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.Id)
            {
                return BadRequest();
            }

            var item = new TicketItem(ticket);
            item.UpdatedAt = DateTime.Now;
            _context.Entry(item).State = EntityState.Modified;

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

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = new TaskItem()
            {
                TaskNo = ticket.TaskNo,
                Title = ticket.Title,
                Desc = ticket.Desc,
                CreatedAt = DateTime.Now
            };
            _context.TaskItem.Add(task);
            await _context.SaveChangesAsync();

            var item = new TicketItem(ticket);
            item.TaskId = task.Id;
            item.CreatedAt = DateTime.Now;
            _context.TicketItem.Add(item);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTicket", new { id = task.Id }, item);
            return await GetTicket(task.Id);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.TicketItem.SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TicketItem.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        private bool itemExists(string id)
        {
            return _context.TicketItem.Any(e => e.Id == id);
        }
    }
}