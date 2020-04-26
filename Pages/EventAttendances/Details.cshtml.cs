using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Authorization;

namespace TRAILES.Pages.EventAttendances
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class DetailsModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public DetailsModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public EventAttendance EventAttendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EventAttendance = await _context.EventAttendances
                .Include(e => e.Event)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EventAttendanceID == id);

            if (EventAttendance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
