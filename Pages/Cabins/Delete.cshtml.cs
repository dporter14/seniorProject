using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.Cabins
{
    public class DeleteModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public DeleteModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cabin Cabin { get; set; }
        public string ErrorMessage {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cabin = await _context.Cabins
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.CabinID == id);

            if (Cabin == null)
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

            var cabin = await _context.Cabins.FindAsync(id);

            if (cabin == null)
            {
                return NotFound();
            }

            try
            {
                _context.Cabins.Remove(Cabin);
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
