using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Controllers
{
    public class EventAttendancesController : Controller
    {
        private readonly TRAILESContext _context;

        public EventAttendancesController(TRAILESContext context)
        {
            _context = context;
        }

        // GET: EventAttendances
        public async Task<IActionResult> Index()
        {
            var tRAILESContext = _context.EventAttendance.Include(e => e.Event).Include(e => e.Student);
            return View(await tRAILESContext.ToListAsync());
        }

        // GET: EventAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventAttendance = await _context.EventAttendance
                .Include(e => e.Event)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventAttendance == null)
            {
                return NotFound();
            }

            return View(eventAttendance);
        }

        // GET: EventAttendances/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "LastName");
            return View();
        }

        // POST: EventAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EventID,StudentID,Priority,Assigned")] EventAttendance eventAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventAttendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "Name", eventAttendance.EventID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "LastName", eventAttendance.StudentID);
            return View(eventAttendance);
        }

        // GET: EventAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventAttendance = await _context.EventAttendance.FindAsync(id);
            if (eventAttendance == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "Name", eventAttendance.EventID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "LastName", eventAttendance.StudentID);
            return View(eventAttendance);
        }

        // POST: EventAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EventID,StudentID,Priority,Assigned")] EventAttendance eventAttendance)
        {
            if (id != eventAttendance.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventAttendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventAttendanceExists(eventAttendance.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "Name", eventAttendance.EventID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "LastName", eventAttendance.StudentID);
            return View(eventAttendance);
        }

        // GET: EventAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventAttendance = await _context.EventAttendance
                .Include(e => e.Event)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventAttendance == null)
            {
                return NotFound();
            }

            return View(eventAttendance);
        }

        // POST: EventAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventAttendance = await _context.EventAttendance.FindAsync(id);
            _context.EventAttendance.Remove(eventAttendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventAttendanceExists(int id)
        {
            return _context.EventAttendance.Any(e => e.ID == id);
        }
    }
}
