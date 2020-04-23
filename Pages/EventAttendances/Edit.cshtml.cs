using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.EventAttendances
{
    public class EditModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public EditModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventAttendance EventAttendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EventAttendance = await _context.EventAttendances
                .Include(e => e.Event)
                .Include(e => e.Student).FirstOrDefaultAsync(m => m.EventAttendanceID == id);

            if (EventAttendance == null)
            {
                return NotFound();
            }
           ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventID");
           ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EventAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventAttendanceExists(EventAttendance.EventAttendanceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventAttendanceExists(int id)
        {
            return _context.EventAttendances.Any(e => e.EventAttendanceID == id);
        }
    }
}
