using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Openccpm.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Tickets
        public async Task<IActionResult> Index( string id )
        {
            if (id == null)
            {
                return View(await _context.TicketView.ToListAsync());
            }
            else
            {
                var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
                if ( project == null )
                {
                    return NotFound();
                }
                return View(await _context.TicketView.Where( x => x.ProjectId == project.Id ).ToListAsync());
            }
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketView = await _context.TicketView
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketView == null)
            {
                return NotFound();
            }

            return View(ticketView);
        }

        // GET: Tickets/Create
        public async Task<IActionResult> Create( string id )
        {
            var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
            ViewData["ProjectId"] = project.Id;
            ViewData["ProjectNo"] = project.ProjectNo;
            createSelectItems();
            return View();
        }

        void createSelectItems()
        {

            ViewData["TrackerItems"] = new SelectList(_context.Tracker.OrderBy(x => x.Position).ToList(), "Id", "Name");
            ViewData["StatusItems"] = new SelectList(_context.Status.OrderBy(x => x.Position).ToList(), "Id", "Name");
            ViewData["PriorityItems"] = new SelectList(_context.Priority.OrderBy(x => x.Position).ToList(), "Id", "Name");
            ViewData["AssignedToItems"] = new SelectList(_context.User.OrderBy(x => x.LastName).ToList(), "Id", "LastName");
        }


        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("TaskNo,Subject,Description,PlanTime,DoneTime,Ticket_Id,Tracker_Id,Tracker_Name,Status_Id,Status_Name,Priority_Id,Priority_Name,AssignedTo_Id,AssignedTo_FirstName,AssignedTo_LastName,Author_Id,Author_FirstName,Author_LastName,DoneRate,Id,Version,CreatedAt,UpdatedAt,Deleted")] TicketView ticketView)
        public async Task<IActionResult> Create([Bind("TaskNo,Subject,Description,PlanTime,DoneTime,Tracker_Id,Status_Id,Priority_Id,AssignedTo_Id,,DoneRate,ProjectId")] TicketView ticketView)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(ticketView);
                //await _context.SaveChangesAsync();

                var task = (TaskItem)ticketView;
                var item = (TicketItem)ticketView;
                task.CreatedAt = DateTime.Now;
                item.CreatedAt = DateTime.Now;

                _context.TaskItem.Add(task);
                item.TaskId = task.Id;
                _context.TicketItem.Add(item);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            return View(ticketView);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketView = await _context.TicketView.SingleOrDefaultAsync(m => m.Id == id);
            if (ticketView == null)
            {
                return NotFound();
            }
            // select用のリストを作成
            createSelectItems();

            return View(ticketView);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaskNo,Subject,Description,PlanTime,DoneTime,ProjectId,Ticket_Id,Ticket_Version,Tracker_Id,Tracker_Name,Status_Id,Status_Name,Priority_Id,Priority_Name,AssignedTo_Id,AssignedTo_FirstName,AssignedTo_LastName,Author_Id,Author_FirstName,Author_LastName,DoneRate,Id,Version,CreatedAt,UpdatedAt,Deleted")] TicketView ticketView)
        {
            if (id != ticketView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TaskItem を更新
                    var item = (TaskItem)ticketView;
                    var ticket = (TicketItem)ticketView;
                    item.UpdatedAt = DateTime.Now;
                    ticket.UpdatedAt = DateTime.Now;
                    _context.Update(item);
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketViewExists(ticketView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = ticketView.ProjectId });
            }
            return View(ticketView);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketView = await _context.TicketView
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketView == null)
            {
                return NotFound();
            }

            return View(ticketView);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ticketView = await _context.TicketView.SingleOrDefaultAsync(m => m.Id == id);
            _context.TicketView.Remove(ticketView);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TicketViewExists(string id)
        {
            return _context.TicketView.Any(e => e.Id == id);
        }
    }

}
