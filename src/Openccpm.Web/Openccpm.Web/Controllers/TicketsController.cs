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
using Openccpm.Lib.Models;

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


        bool IsIncludeProject( string id )
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var cnt = _context.ProjectUserView
                .Where(x => x.ProjectId == id || x.ProjectNo == id)
                .Where(x => x.UserId == userId)
                .Count();
            return cnt > 0;
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
                if (!IsIncludeProject(id))
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
                    query = query.Where(x => x.TrackerId == trackerid);
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
            var taskNo = "TK001";
            try
            {
                var maxTicket = await _context.TicketView
                    .Where(x => x.ProjectId == project.Id)
                    .MaxAsync(x => x.TicketNo);
                if (!string.IsNullOrEmpty(maxTicket))
                {
                    taskNo = string.Format("TK{0:000}", int.Parse(maxTicket.Substring(2)) + 1);
                }
            } catch { }
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var tk = new TicketView()
            {
                TicketNo = taskNo,
                DoneRate = 0,
                ProjectId = project.Id, Project = new Project() {  Id = project.Id, ProjectNo = project.ProjectNo, Name = project.Name },
                TrackerId = _context.Tracker.SingleOrDefault(x => x.Name == "機能").Id,
                StatusId = _context.Status.SingleOrDefault(x => x.Name == "新規").Id,
                PriorityId = _context.Priority.SingleOrDefault( x => x.Name == "標準" ).Id,
                AssignedToId = userId,
                AuthorId = userId,
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
            var q =
                from pu in _context.ProjectUser
                join p in _context.Project on pu.ProjectId equals p.Id
                join u in _context.Users on pu.UserId equals u.Id
                where p.Id == projectId || p.ProjectNo == projectId
                orderby u.UserName
                select new User()
                {
                    Id = u.Id,
                    UserName = u.UserName
                };
            var items = q.ToList();
            ViewData["AssignedToItems"] = new SelectList( q.ToList(), "Id", "UserName");
        }


        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketNo,Subject,Description,PlanTime,DoneTime,TrackerId,StatusId,PriorityId,AssignedToId,,DoneRate,ProjectId,DueDate,StartDate")] TicketItem item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt = DateTime.Now;
                _context.TicketItem.Add(item);
                await _context.SaveChangesAsync();

                var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == item.ProjectId);

                return RedirectToAction("Index", new { id = project.ProjectNo });
            }
            return View(item);
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
        public async Task<IActionResult> Edit(string id, [Bind("TicketNo,Subject,Description,PlanTime,DoneTime,ProjectId,TicketId,TrackerId,StatusId,PriorityId,AssignedToId,AuthorId,DoneRate,Id,Version,CreatedAt,UpdatedAt,Deleted,DueDate,StartDate")] TicketItem item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TaskItem を更新
                    item.UpdatedAt = DateTime.Now;
                    _context.TicketItem.Update(item);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketViewExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var project = await _context.Project.SingleOrDefaultAsync(x => x.Id == item.ProjectId);
                return RedirectToAction("Index", new { id = project.ProjectNo });
            }
            return View(item);
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
            var item = await _context.TicketItem.SingleOrDefaultAsync(m => m.Id == id);
            item.Deleted = true;
            _context.TicketItem.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TicketViewExists(string id)
        {
            return _context.TicketItem.Any(e => e.Id == id);
        }
    }
}
