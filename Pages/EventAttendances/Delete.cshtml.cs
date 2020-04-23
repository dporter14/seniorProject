using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.EventAttendances
{
    public class DeleteModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public DeleteModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventAttendance EventAttendance { get; set; }
        public string ErrorMessage {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            EventAttendance = await _context.EventAttendances
                .Include(e => e.Event)
                .Include(e => e.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EventAttendanceID == id);

            if (EventAttendance == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventattendance = await _context.EventAttendances.FindAsync(id);

            if (eventattendance == null)
            {
                return NotFound();
            }

            try
            {
                _context.EventAttendances.Remove(EventAttendance);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction("./Delete", new {id, saveChangesError = true});
            }
        }
    }
}
