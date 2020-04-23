using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Pages.Students
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
        ViewData["CabinID"] = new SelectList(_context.Cabins, "CabinID", "CabinID");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStudent = new Student();

            if (await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",
                s => s.Fname,
                s => s.Lname,
                s => s.Gender,
                s => s.GradeLevel,
                s => s.Registered,
                s => s.Email
            ))
            {
                emptyStudent.UserName = emptyStudent.Email;
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();

        }
    }
}
