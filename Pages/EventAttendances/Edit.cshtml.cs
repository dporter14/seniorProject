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
using Microsoft.AspNetCore.Authorization;

namespace TRAILES.Pages.EventAttendances
{
    [Authorize(Policy = "RequireAdministratorRole")]
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

            EventAttendance = await _context.EventAttendances.FindAsync(id);

            if (EventAttendance == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int  id)
        {
            var EventAttendanceToUpdate = await _context.EventAttendances.FindAsync(id);
            if (EventAttendanceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<EventAttendance>(
                EventAttendanceToUpdate,
                "eventattendance",
                e => e.Priority, e=> e.Assigned, e => e.Weight, e => e.EventID, e => e.StudentID
            ))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool EventAttendanceExists(int id)
        {
            return _context.EventAttendances.Any(e => e.EventAttendanceID == id);
        }
    }
}
