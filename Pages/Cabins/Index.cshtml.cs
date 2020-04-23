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
    public class IndexModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public IndexModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Cabin> Cabin { get;set; }

        public async Task OnGetAsync()
        {
            Cabin = await _context.Cabins.ToListAsync();
        }
    }
}
