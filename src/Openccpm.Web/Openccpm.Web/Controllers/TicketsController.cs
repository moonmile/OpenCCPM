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

        public IActionResult Select()
        {
            ViewData["ProjectNo"] = "";
            ViewData["ProjectName"] = "�������̃`�P�b�g";

            var items = _context.TicketView.AsQueryable();
            return View("Index", items);
        }


        // GET: Tickets
        public async Task<IActionResult> Index( string id, [FromQuery] string trackerid, [FromQuery] int? isclosed )
        {
            if (id == null)
            {
                // �v���W�F�N�gID/�ԍ����w�肵�ĂȂ��ꍇ�́A�v���W�F�N�g�ɑ����Ă��Ȃ��`�P�b�g��\������
                ViewData["ProjectNo"] = "";
                ViewData["ProjectName"] = "�������̃`�P�b�g";
                return View(await _context.TicketView
                    .Where( x => x.ProjectId == null )         
                    .ToListAsync());
            }
            else
            {
                var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
                if ( project == null )
                {
                    return NotFound();
                }
                ViewData["ProjectNo"] = project.ProjectNo;
                ViewData["ProjectName"] = project.Name;

                var query = _context
                    .TicketView.Where(x => x.ProjectId == project.Id );
                if (trackerid != null) {
                    query = query.Where(x => x.Tracker_Id == trackerid);
                }
                if ( isclosed != null )
                {
                    if ( isclosed.Value == 0 )
                    {
                        query = query.Where(x => x.Status_IsClosed == false);

                    } else
                    {
                        query = query.Where(x => x.Status_IsClosed == true);
                    }
                }
                query = query.OrderByDescending(x => x.CreatedAt);
                var items = await query.ToListAsync();
                return View(items);
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

            ViewData["ProjectId"] = ticketView.ProjectId;
            ViewData["ProjectNo"] = ticketView.Project_ProjectNo;
            ViewData["ProjectName"] = ticketView.Project_Name;

            return View(ticketView);
        }

        // GET: Tickets/Create
        public async Task<IActionResult> Create( string id )
        {
            var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
            ViewData["ProjectId"] = project.Id;
            ViewData["ProjectNo"] = project.ProjectNo;
            ViewData["ProjectName"] = project.Name;
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
        public async Task<IActionResult> Create([Bind("TaskNo,Subject,Description,PlanTime,DoneTime,Tracker_Id,Status_Id,Priority_Id,AssignedTo_Id,,DoneRate,ProjectId,Project_ProjectNo")] TicketView ticketView)
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

                return RedirectToAction("Index", new { id = ticketView.Project_ProjectNo });
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
            // select�p�̃��X�g���쐬
            createSelectItems();
            ViewData["ProjectId"] = ticketView.ProjectId;
            ViewData["ProjectNo"] = ticketView.Project_ProjectNo;
            ViewData["ProjectName"] = ticketView.Project_Name;

            return View(ticketView);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaskNo,Subject,Description,PlanTime,DoneTime,ProjectId,Ticket_Id,Ticket_Version,Tracker_Id,Tracker_Name,Status_Id,Status_Name,Priority_Id,Priority_Name,AssignedTo_Id,AssignedTo_FirstName,AssignedTo_LastName,Author_Id,Author_FirstName,Author_LastName,DoneRate,Id,Version,CreatedAt,UpdatedAt,Deleted,Project_Name,Project_ProjectNo")] TicketView ticketView)
        {
            if (id != ticketView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TaskItem ���X�V
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
                var projectNo = ticketView.Project_ProjectNo;
                return RedirectToAction("Index", new { id = projectNo });
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