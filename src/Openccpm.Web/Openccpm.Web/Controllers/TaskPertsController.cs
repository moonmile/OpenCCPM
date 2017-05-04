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
    public class TaskPertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskPertsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TaskPerts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskPert.ToListAsync());
        }

        // GET: TaskPerts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPert = await _context.TaskPert
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskPert == null)
            {
                return NotFound();
            }

            return View(taskPert);
        }

        // GET: TaskPerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskPerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PreTaskId,PostTaskId,Id,Version,CreatedAt,UpdatedAt,Deleted")] TaskPert taskPert)
        {
            if (ModelState.IsValid)
            {
                taskPert.CreatedAt = DateTime.Now;
                _context.Add(taskPert);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskPert);
        }

        // GET: TaskPerts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPert = await _context.TaskPert.SingleOrDefaultAsync(m => m.Id == id);
            if (taskPert == null)
            {
                return NotFound();
            }
            return View(taskPert);
        }

        // POST: TaskPerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PreTaskId,PostTaskId,Id,Version,CreatedAt,UpdatedAt,Deleted")] TaskPert taskPert)
        {
            if (id != taskPert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taskPert.UpdatedAt = DateTime.Now;
                    _context.Update(taskPert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskPertExists(taskPert.Id))
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
            return View(taskPert);
        }

        // GET: TaskPerts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPert = await _context.TaskPert
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskPert == null)
            {
                return NotFound();
            }

            return View(taskPert);
        }

        // POST: TaskPerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var taskPert = await _context.TaskPert.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaskPert.Remove(taskPert);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaskPertExists(string id)
        {
            return _context.TaskPert.Any(e => e.Id == id);
        }
    }
}
