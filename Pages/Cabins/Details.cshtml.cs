using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;
using Microsoft.AspNetCore.Identity;

namespace TRAILES.Pages.Cabins
{
    public class DetailsModel : PageModel
    {
        private readonly TRAILES.Data.AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public DetailsModel(TRAILES.Data.AppDbContext context,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var cabin = await _context.Cabins.FindAsync(id);
            if (user == null || cabin == null)
            {
                return NotFound();
            }

            var studentToReg = await _context.Students.FindAsync(user.Id);
            if (studentToReg == null 
                || studentToReg.Registered 
                || studentToReg.Gender != cabin.Gender
                || cabin.BedsRegistered == cabin.BedCount)
            {
                return Forbid();
            }

            var oldCabin = await _context.Cabins.FindAsync(studentToReg.CabinID);

            if (await TryUpdateModelAsync<Student>(
                studentToReg,
                "student",
                s => s.CabinID
            ))
            {
                oldCabin.BedsRegistered--;
                studentToReg.Registered = true;
                studentToReg.CabinID = id;
                cabin.BedsRegistered++;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
