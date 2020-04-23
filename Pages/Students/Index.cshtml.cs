using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public IndexModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string GenderSort { get; set; }
        public string RegisteredSort{ get; set; }
        public IList<Student> Students { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            RegisteredSort = sortOrder == "Registered" ? "reg_desc" : "Registered";

            IQueryable<Student> studentsIQ = from s in _context.Students
                                                select s;

            switch(sortOrder)
            {
                case "reg_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Registered);
                    break;
                case "Registered":
                    studentsIQ = studentsIQ.OrderBy(s => s.Registered);
                    break;
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Lname);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.Lname);
                    break;
            }

            Students = await studentsIQ
            .Include(s => s.Cabin)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}
