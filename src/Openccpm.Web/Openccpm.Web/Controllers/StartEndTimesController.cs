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
    public class StartEndTimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StartEndTimesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: StartEndTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StartEndTime.ToListAsync());
        }

        // GET: StartEndTimes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startEndTime = await _context.StartEndTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (startEndTime == null)
            {
                return NotFound();
            }

            return View(startEndTime);
        }

        // GET: StartEndTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StartEndTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,StartAt,EndAt,IsPlan,Id,Version,CreatedAt,UpdatedAt,Deleted")] StartEndTime startEndTime)
        {
            if (ModelState.IsValid)
            {
                startEndTime.CreatedAt = DateTime.Now;
                _context.Add(startEndTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(startEndTime);
        }

        // GET: StartEndTimes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startEndTime = await _context.StartEndTime.SingleOrDefaultAsync(m => m.Id == id);
            if (startEndTime == null)
            {
                return NotFound();
            }
            return View(startEndTime);
        }

        // POST: StartEndTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaskId,StartAt,EndAt,IsPlan,Id,Version,CreatedAt,UpdatedAt,Deleted")] StartEndTime startEndTime)
        {
            if (id != startEndTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    startEndTime.UpdatedAt = DateTime.Now;
                    _context.Update(startEndTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StartEndTimeExists(startEndTime.Id))
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
            return View(startEndTime);
        }

        // GET: StartEndTimes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startEndTime = await _context.StartEndTime
                .SingleOrDefaultAsync(m => m.Id == id);
            if (startEndTime == null)
            {
                return NotFound();
            }

            return View(startEndTime);
        }

        // POST: StartEndTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var startEndTime = await _context.StartEndTime.SingleOrDefaultAsync(m => m.Id == id);
            _context.StartEndTime.Remove(startEndTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StartEndTimeExists(string id)
        {
            return _context.StartEndTime.Any(e => e.Id == id);
        }
    }
}
