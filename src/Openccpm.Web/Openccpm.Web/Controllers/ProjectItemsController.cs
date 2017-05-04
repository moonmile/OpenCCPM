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
    public class ProjectItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectItemsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProjectItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectItem.ToListAsync());
        }

        // GET: ProjectItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectItem = await _context.ProjectItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectItem == null)
            {
                return NotFound();
            }

            return View(projectItem);
        }

        // GET: ProjectItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,CreateAt,Completed")] ProjectItem projectItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectItem);
        }

        // GET: ProjectItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectItem = await _context.ProjectItem.SingleOrDefaultAsync(m => m.Id == id);
            if (projectItem == null)
            {
                return NotFound();
            }
            return View(projectItem);
        }

        // POST: ProjectItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,CreateAt,Completed")] ProjectItem projectItem)
        {
            if (id != projectItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectItemExists(projectItem.Id))
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
            return View(projectItem);
        }

        // GET: ProjectItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectItem = await _context.ProjectItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projectItem == null)
            {
                return NotFound();
            }

            return View(projectItem);
        }

        // POST: ProjectItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectItem = await _context.ProjectItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProjectItem.Remove(projectItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectItemExists(int id)
        {
            return _context.ProjectItem.Any(e => e.Id == id);
        }
    }
}
