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
using Microsoft.AspNetCore.Authorization;

namespace TRAILES.Pages.Students
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class EditModel : CabinNamePageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;

        public EditModel(TRAILES.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                .Include(c => c.Cabin)
                .FirstOrDefaultAsync(c => c.Id == id);
                //.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            PopulateCabinsDropDownList(_context, Student.CabinID);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var StudentToUpdate = await _context.Students.FindAsync(id);

            if (StudentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Student>(
                StudentToUpdate,
                "student",
                s => s.Fname,
                s => s.Lname,
                s => s.Gender,
                s => s.GradeLevel,
                s => s.Registered,
                s => s.Email,
                s => s.CabinID
            ))
            {
                StudentToUpdate.UserName = StudentToUpdate.Email;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateCabinsDropDownList(_context, StudentToUpdate.CabinID);
            return Page();
        }
    }
}
