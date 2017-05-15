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

namespace Openccpm.Web.Controllers
{
    [Authorize(Roles = "ProjectMembers")]
    [Produces("application/json")]
    [Route("api/Ticket")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ticket/Project/1
        [HttpGet("Project/{id}")]
        public IEnumerable<TicketView> GetTicketsInProject( string id )
        {
            return _context.TicketView
                .Where( x => x.ProjectId == id )
                .OrderByDescending( x => x.CreatedAt );
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.TicketView.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            // ナビゲーションの取得
            item.Tracker = await _context.Tracker.SingleOrDefaultAsync(x => x.Id == item.Tracker_Id);
            item.Status = await _context.Status.SingleOrDefaultAsync(x => x.Id == item.Status_Id);
            item.Priority = await _context.Priority.SingleOrDefaultAsync(x => x.Id == item.Priority_Id);
            item.AssignedTo = await _context.User.SingleOrDefaultAsync(x => x.Id == item.AssignedTo_Id);
            item.Author = await _context.User.SingleOrDefaultAsync(x => x.Id == item.Author_Id);

            return Ok(item);
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] string id, [FromBody] TicketView ticketView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketView.Id)
            {
                return BadRequest();
            }

            var item = (TaskItem)ticketView;
            var ticket = (TicketItem)ticketView;
            item.UpdatedAt = DateTime.Now;
            ticket.UpdatedAt = DateTime.Now;
            _context.Update(item);
            _context.Update(ticket);

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
        public async Task<IActionResult> PostTicket([FromBody] TicketView ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = (TaskItem)ticket;
            var item = (TicketItem)ticket;
            task.CreatedAt = DateTime.Now;
            item.CreatedAt = DateTime.Now;

            _context.TaskItem.Add(task);
            item.TaskId = task.Id;
            _context.TicketItem.Add(item);
            await _context.SaveChangesAsync();

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

            var item = await _context.TaskItem.SingleOrDefaultAsync(m => m.Id == id);
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
            return _context.TicketItem.Any(e => e.Id == id);
        }
    }
}