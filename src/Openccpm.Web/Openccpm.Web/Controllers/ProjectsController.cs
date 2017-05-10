using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;

namespace Openccpm.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Project.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .SingleOrDefaultAsync(m => m.Id == id || m.ProjectNo == id );
            if (project == null)
            {
                return NotFound();
            }

            // チケットのサマリを計算する
            // Azure 上だと DbSet を直接 JOIN できないので、あらかじめ List で持って来る。
            // データ量が心配なので、ここは SQL 直書きにする予定。
            var trackers = _context.Tracker.OrderBy(x => x.Position).ToList();
            var status = _context.Status.OrderBy(x => x.Position).ToList();
            var ticketviews = _context.TicketView.Where(x => x.ProjectId == project.Id).ToList();

            var lst = new List<Tuple<string, string, int, int, int>>();
            foreach (var tr in trackers)
            {
                var trId = tr.Id;

                var name = tr.Name;
                var cnt1 = (from ti in ticketviews
                            join st in status on ti.Status_Id equals st.Id 
                            where ti.ProjectId == project.Id &&
                                    ti.Tracker_Id == tr.Id &&
                                    st.IsClosed == false
                            select ti.Id).Count();
                var cnt2 = (from ti in ticketviews
                            join st in status on ti.Status_Id equals st.Id
                            where ti.ProjectId == project.Id &&
                                    ti.Tracker_Id == tr.Id &&
                                    st.IsClosed == true
                            select ti.Id).Count();

                lst.Add(new Tuple<string, string, int, int, int>(tr.Id, tr.Name, cnt1, cnt2, cnt1 + cnt2));
            }
            ViewData["TicketSum"] = lst;
            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectNo,Name,Description,Id,Version,CreatedAt,UpdatedAt,Deleted")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedAt = DateTime.Now;
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProjectNo,Name,Description,Id,Version,CreatedAt,UpdatedAt,Deleted")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    project.UpdatedAt = DateTime.Now;
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectExists(string id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
