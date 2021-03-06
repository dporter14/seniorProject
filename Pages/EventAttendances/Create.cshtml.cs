using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Authorization;

namespace TRAILES.Pages.EventAttendances
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class CreateModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public CreateModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventID");
        ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public EventAttendance EventAttendance { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyEventAttendance = new EventAttendance();
            if (await TryUpdateModelAsync<EventAttendance>(
                emptyEventAttendance,
                "eventattendance",
                e => e.Priority, e=> e.Assigned, e => e.Weight, e => e.EventID, e => e.StudentID
            ))
            {
                _context.EventAttendances.Add(emptyEventAttendance);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }
    }
}
