﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Data;
using Openccpm.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Openccpm.Lib.Models;

namespace Openccpm.Web.Controllers
{
    [Authorize(Roles = "ProjectMembers")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager     )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Projects
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var items = await _context.Project
                .Where(x => x.Deleted == false)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return View(items);
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "ProjectMembers")]
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
            // 自分の所属するプロジェクトだけを表示する
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var res = _context.ProjectUser.SingleOrDefault(x => x.ProjectId == project.Id && x.UserId == userId);
            if ( res == null )
            {
                return NotFound();
            }

            // チケットのサマリを計算する
            var trackers = _context.Tracker.OrderBy(x => x.Position).ToList();

            var lst = new List<Tuple<string, string, int, int, int>>();
            foreach (var tr in trackers)
            {
                var trId = tr.Id;

                var name = tr.Name;
                var cnt1 = (from ti in _context.TicketView
                            where ti.ProjectId == project.Id &&
                                    ti.TrackerId == tr.Id &&
                                    ti.Status_IsClosed == false
                            select ti.Id).Count();
                var cnt2 = (from ti in _context.TicketView
                            where ti.ProjectId == project.Id &&
                                    ti.TrackerId == tr.Id &&
                                    ti.Status_IsClosed == true
                            select ti.Id).Count();

                lst.Add(new Tuple<string, string, int, int, int>(tr.Id, tr.Name, cnt1, cnt2, cnt1 + cnt2));
            }
            ViewData["TicketSum"] = lst;
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "ProjectAdministrators")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ProjectAdministrators")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectNo,Name,Description,Id,Version,CreatedAt,UpdatedAt,Deleted")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedAt = DateTime.Now;
                _context.Add(project);
                await _context.SaveChangesAsync();

                // 自分をプロジェクトに追加して置く
                var userId = _userManager.GetUserId(Request.HttpContext.User);
                _context.Add(new ProjectUser() { ProjectId = project.Id, UserId = userId });
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "ProjectAdministrators")]
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
        [Authorize(Roles = "ProjectAdministrators")]
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
        [Authorize(Roles = "ProjectAdministrators")]
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
        [Authorize(Roles = "ProjectAdministrators")]
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
