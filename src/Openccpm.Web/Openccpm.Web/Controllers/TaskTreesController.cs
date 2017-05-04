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
    public class TaskTreesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskTreesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TaskTrees
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskTree.ToListAsync());
        }

        // GET: TaskTrees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTree = await _context.TaskTree
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskTree == null)
            {
                return NotFound();
            }

            return View(taskTree);
        }

        // GET: TaskTrees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskTrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentTaskId,ChildTaskId,Id,Version,CreatedAt,UpdatedAt,Deleted")] TaskTree taskTree)
        {
            if (ModelState.IsValid)
            {
                taskTree.CreatedAt = DateTime.Now;
                _context.Add(taskTree);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskTree);
        }

        // GET: TaskTrees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTree = await _context.TaskTree.SingleOrDefaultAsync(m => m.Id == id);
            if (taskTree == null)
            {
                return NotFound();
            }
            return View(taskTree);
        }

        // POST: TaskTrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ParentTaskId,ChildTaskId,Id,Version,CreatedAt,UpdatedAt,Deleted")] TaskTree taskTree)
        {
            if (id != taskTree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taskTree.UpdatedAt = DateTime.Now;
                    _context.Update(taskTree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskTreeExists(taskTree.Id))
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
            return View(taskTree);
        }

        // GET: TaskTrees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskTree = await _context.TaskTree
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskTree == null)
            {
                return NotFound();
            }

            return View(taskTree);
        }

        // POST: TaskTrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var taskTree = await _context.TaskTree.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaskTree.Remove(taskTree);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaskTreeExists(string id)
        {
            return _context.TaskTree.Any(e => e.Id == id);
        }
    }
}
