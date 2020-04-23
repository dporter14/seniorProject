using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.Cabins
{
    public class CreateModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public CreateModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cabin Cabin { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cabins.Add(Cabin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
