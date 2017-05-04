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
    public class ProjectViewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectViewsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProjectViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectView.ToListAsync());
        }

        // GET: ProjectViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectView = await _context.ProjectView
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectView == null)
            {
                return NotFound();
            }

            return View(projectView);
        }

        // GET: ProjectViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name")] ProjectView projectView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectView);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectView);
        }

        // GET: ProjectViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectView = await _context.ProjectView.SingleOrDefaultAsync(m => m.Id == id);
            if (projectView == null)
            {
                return NotFound();
            }
            return View(projectView);
        }

        // POST: ProjectViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name")] ProjectView projectView)
        {
            if (id != projectView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectViewExists(projectView.Id))
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
            return View(projectView);
        }

        // GET: ProjectViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectView = await _context.ProjectView
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectView == null)
            {
                return NotFound();
            }

            return View(projectView);
        }

        // POST: ProjectViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectView = await _context.ProjectView.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProjectView.Remove(projectView);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectViewExists(int id)
        {
            return _context.ProjectView.Any(e => e.Id == id);
        }
    }
}
