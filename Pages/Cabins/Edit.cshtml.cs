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

namespace TRAILES.Pages.Cabins
{
    public class EditModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public EditModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cabin Cabin { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cabin = await _context.Cabins.FindAsync(id);

            if (Cabin == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int  id)
        {
            var CabinToUpdate = await _context.Cabins.FindAsync(id);
            if (CabinToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Cabin>(
                CabinToUpdate,
                "cabin",
                c => c.Name,
                c => c.Gender,
                c => c.BedCount,
                c => c.BedsRegistered,
                c => c.Chapperone
            ))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool CabinExists(int id)
        {
            return _context.Cabins.Any(e => e.CabinID == id);
        }
    }
}
