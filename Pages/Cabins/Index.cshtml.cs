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

        public string NameSort {get; set;}
        public string CountSort {get; set;}
        public IList<Cabin> Cabin { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "" : "";
            CountSort = sortOrder == "BedsRegistered" ? "reg_desc" : "BedsRegistered";

            var username = User.Identity.Name;
            if (String.IsNullOrEmpty(username))
            {
                RedirectToPage("/Index");
            }
            var user = await _context.Students
                .Where(s => s.UserName == username)
                .FirstOrDefaultAsync();
            
            if (user == null)
            {
                RedirectToPage("/Index");
            }

            IQueryable<Cabin> cabinsIQ = from c in _context.Cabins 
                                            select c;

            switch(sortOrder)
            {
                case "reg_desc":
                    cabinsIQ = cabinsIQ.OrderByDescending(c => c.BedsRegistered);
                    break;
                case "BedsRegistered":
                    cabinsIQ = cabinsIQ.OrderBy(c => c.BedsRegistered);
                    break;
                default:
                    cabinsIQ = cabinsIQ.OrderBy(c => c.Name);
                    break;
            }
            if (User.Identity.Name != "admin@bchstest.com")
            {
                Cabin = await cabinsIQ
                    .Where(c => c.Gender == user.Gender)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                Cabin = await cabinsIQ
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
