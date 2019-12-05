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
            var tRAILESContext = _context.EventAttendance.Include(e => e.Event).Include(e => e.Student).OrderByDescending(e => e.Assigned);

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
                foreach (var student in _context.Student.Where(s => s.ID == eventAttendance.StudentID))
                {
                    eventAttendance.Priority = student.priorityRemaining;
                    student.priorityRemaining += 1;
                    _context.Update(student);
                }
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

        public async Task<IActionResult> Schedule()
        {
            if (ModelState.IsValid)
            {
                var events = _context.Event.Select(e => e);
                int[] prtyWghts = new int[events.Count() + 1];
                int[] grdWghts = new int[13];
                for (int i = 0; i < events.Count() + 1; i++)
                {
                    prtyWghts[i] = 1 - (i / (events.Count() + 1));
                }
                int j = 0;
                for (int i = 12; i > 0; i--)
                {
                    grdWghts[i] = 1 - ((j) / (5));
                    if (j < 5)
                        j++;
                }
                foreach (var e in events)
                {
                    var att =  _context.EventAttendance.Select(ea => ea);
                    var usr = _context.Student.Select(u => u);
                    foreach (var st in att.Where(ea => ea.EventID == e.ID))
                    {
                        var one = Math.Pow(prtyWghts[st.Priority], 2);
                        var two = Math.Pow(grdWghts[usr.Where(u => u.ID == st.StudentID).Select(u => u.GradeLevel).Single()], 2);
                        var three = Math.Pow(prtyWghts[usr.Where(u => u.ID == st.StudentID).Select(u=> u.priorityRemaining).Single()], 2);
                        var wght = Math.Sqrt(one + two + three);
                        st.weight = wght;
                    }
                    var stud = att.OrderBy(st => st.weight);
                    int signed = 0;
                    foreach (var t in stud)
                    {
                        if (signed < e.MaxAttendance)
                        {
                            t.Assigned = true;
                            signed++;
                        }
                        else
                        {
                            foreach (var us in usr.Where(u => u.ID == t.StudentID))
                            {
                                if (us.priorityRemaining > 1)
                                    us.priorityRemaining -= 1;
                                _context.Update(us);
                            }
                            t.Assigned = false;
                        }
                        _context.Update(t);
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
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
