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
    public class DetailsModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public DetailsModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public Cabin Cabin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cabin = await _context.Cabins
            .FirstOrDefaultAsync(m => m.CabinID == id);

            Cabin.Students = await _context.Students
            .Where(s => s.CabinID == id)
            .ToListAsync();
            

            if (Cabin == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
