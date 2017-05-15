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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Openccpm.Web.Controllers
{
    [Authorize(Roles = "ProjectMembers")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TicketsController(
            ApplicationDbContext context,
            UserManager< ApplicationUser > userManager,
            SignInManager<ApplicationUser> signInManager     )
        {
                _context = context;
                _userManager = userManager;
                _signInManager = signInManager;
        }

        public IActionResult Select()
        {
            ViewData["ProjectNo"] = "";
            ViewData["ProjectName"] = "未所属のチケット";

            var items = _context.TicketView.AsQueryable();
            return View("Index", items);
        }


        // GET: Tickets
        public async Task<IActionResult> Index( string id, [FromQuery] string trackerid, [FromQuery] int? isclosed )
        {
            if (id == null)
            {
                // プロジェクトID/番号を指定してない場合は、プロジェクトに属していないチケットを表示する
                ViewData["ProjectNo"] = "";
                ViewData["ProjectName"] = "未所属のチケット";
                return View(await _context.TicketView
                    .Where( x => x.ProjectId == null ) 
                    .ToListAsync());
            }
            else
            {
                // 自分の所属するプロジェクトだけを表示する
                var userId = _userManager.GetUserId(Request.HttpContext.User);
                var cnt = _context.ProjectUserView
                    .Where(x => x.ProjectId == id || x.ProjectNo == id)
                    .Where(x => x.UserId == userId)
                    .Count();
                if (cnt == 0)
                {
                    return NotFound();
                }

                var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == id || x.ProjectNo == id);
                if ( project == null )
                {
                    return NotFound();
                }
                ViewData["ProjectNo"] = project.ProjectNo;
                ViewData["ProjectName"] = project.Name;

                var query = _context.TicketView
                    .Where(x => x.ProjectId == project.Id);

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
            createSelectItems( project.Id  );

            // チケット番号の初期値を入れる
            var maxTicket = await _context.TicketView
                .Where( x => x.ProjectId == project.Id )
                .MaxAsync(x => x.TaskNo);
            var taskNo = "TK001";
            if ( !string.IsNullOrEmpty( maxTicket ))
            {
                taskNo = string.Format("TK{0:000}", int.Parse(maxTicket.Substring(2)) + 1);
            }
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var tk = new TicketView()
            {
                TaskNo = taskNo,
                DoneRate = 0,
                ProjectId = project.Id,
                Project_Name = project.Name,
                Project_ProjectNo = project.ProjectNo,
                Tracker_Id = _context.Tracker.SingleOrDefault(x => x.Name == "機能").Id,
                Status_Id = _context.Status.SingleOrDefault(x => x.Name == "新規").Id,
                Priority_Id = _context.Priority.SingleOrDefault( x => x.Name == "標準" ).Id,
                AssignedTo_Id = userId,
                PlanTime = 1,
                DoneTime = 0
                
            };
            return View(tk);
        }

        /// <summary>
        /// プロジェクトIDを指定して、各選択リストを取得する
        /// </summary>
        /// <param name="projectId"></param>
        void createSelectItems( string projectId )
        {

            ViewData["TrackerItems"] = new SelectList(_context.Tracker.OrderBy(x => x.Position).ToList(), "Id", "Name");
            ViewData["StatusItems"] = new SelectList(_context.Status.OrderBy(x => x.Position).ToList(), "Id", "Name");
            ViewData["PriorityItems"] = new SelectList(_context.Priority.OrderBy(x => x.Position).ToList(), "Id", "Name");
            // プロジェクトの所属している担当者のみ返す
            ViewData["AssignedToItems"] = new SelectList( _context.ProjectUserView.Where( x => x.ProjectId == projectId ) 
                .OrderBy(x => x.UserName)
                .ToList(), "Id", "UserName");
        }


        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskNo,Subject,Description,PlanTime,DoneTime,Tracker_Id,Status_Id,Priority_Id,AssignedTo_Id,,DoneRate,ProjectId,Project_ProjectNo,DueDate,StartDate")] TicketView ticketView)
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
                await _context.SaveChangesAsync();

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
            // select用のリストを作成
            createSelectItems(ticketView.ProjectId);
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
        public async Task<IActionResult> Edit(string id, [Bind("TaskNo,Subject,Description,PlanTime,DoneTime,ProjectId,Ticket_Id,Ticket_Version,Tracker_Id,Tracker_Name,Status_Id,Status_Name,Priority_Id,Priority_Name,AssignedTo_Id,AssignedTo_FirstName,AssignedTo_LastName,Author_Id,Author_FirstName,Author_LastName,DoneRate,Id,Version,CreatedAt,UpdatedAt,Deleted,Project_Name,Project_ProjectNo,StartEndTime_Id,StartEndTime_Version,DueDate,StartDate")] TicketView ticketView)
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
            var task = await _context.TaskItem.SingleOrDefaultAsync(m => m.Id == ticketView.Id);
            task.Deleted = true;
            _context.TaskItem.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TicketViewExists(string id)
        {
            return _context.TicketView.Any(e => e.Id == id);
        }
    }

}
