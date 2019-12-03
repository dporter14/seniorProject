using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAILES.Data;
using TRAILES.Models;

namespace TRAILES.Controllers
{
    public class FacStaffController : Controller
    {
        private readonly TRAILESContext _context;

        public FacStaffController(TRAILESContext context)
        {
            _context = context;
        }

        // GET: FacStaff
        public async Task<IActionResult> Index()
        {
            var tRAILESContext = _context.FacStaff.Include(f => f.Cabin);
            return View(await tRAILESContext.ToListAsync());
        }

        // GET: FacStaff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facStaff = await _context.FacStaff
                .Include(f => f.Cabin)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (facStaff == null)
            {
                return NotFound();
            }

            return View(facStaff);
        }

        // GET: FacStaff/Create
        public IActionResult Create()
        {
            ViewData["CabinID"] = new SelectList(_context.Cabin, "ID", "ID");
            return View();
        }

        // POST: FacStaff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMid,Admin,CabinID")] FacStaff facStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CabinID"] = new SelectList(_context.Cabin, "ID", "ID", facStaff.CabinID);
            return View(facStaff);
        }

        // GET: FacStaff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facStaff = await _context.FacStaff.FindAsync(id);
            if (facStaff == null)
            {
                return NotFound();
            }
            ViewData["CabinID"] = new SelectList(_context.Cabin, "ID", "ID", facStaff.CabinID);
            return View(facStaff);
        }

        // POST: FacStaff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMid,Admin,CabinID")] FacStaff facStaff)
        {
            if (id != facStaff.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacStaffExists(facStaff.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CabinID"] = new SelectList(_context.Cabin, "ID", "ID", facStaff.CabinID);
            return View(facStaff);
        }

        // GET: FacStaff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facStaff = await _context.FacStaff
                .Include(f => f.Cabin)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (facStaff == null)
            {
                return NotFound();
            }

            return View(facStaff);
        }

        // POST: FacStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facStaff = await _context.FacStaff.FindAsync(id);
            _context.FacStaff.Remove(facStaff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacStaffExists(int id)
        {
            return _context.FacStaff.Any(e => e.ID == id);
        }
    }
}
