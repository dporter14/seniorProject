using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Identity;

namespace TRAILES.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(TRAILES.Data.AppDbContext context,
                            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
            .Include(e => e.EventAttendances)
            .ThenInclude(s => s.Student)
            .FirstOrDefaultAsync(m => m.EventID == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var evnt = await _context.Events.FindAsync(id);
            if (user == null || evnt == null)
            {
                return NotFound();
            }

            var studentToAdd = await _context.Students.FindAsync(user.Id);

            var exists = await _context.EventAttendances
                .FirstOrDefaultAsync(x => x.StudentID == studentToAdd.Id
                                    && x.EventID == evnt.EventID);
            if (studentToAdd == null
                || exists != null)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Event>(
                evnt,
                "event",
                e => e.EventID
            ))
            {
                studentToAdd.priorityRemaining++;
                var eventAtt = new EventAttendance
                {
                    Priority = studentToAdd.priorityRemaining,
                    Event = evnt,
                    EventID = evnt.EventID,
                    Student = studentToAdd,
                    StudentID = studentToAdd.Id
                };
                await _context.EventAttendances.AddAsync(eventAtt);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
