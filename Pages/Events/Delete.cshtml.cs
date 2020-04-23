using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public DeleteModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }
        public string ErrorMessage {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.EventID == id);

            if (Event == null)
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

            var eve = await _context.Events.FindAsync(id);

            if (eve == null)
            {
                return NotFound();
            }

            try
            {
                _context.Events.Remove(Event);
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