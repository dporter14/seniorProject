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
    public class CabinsController : Controller
    {
        private readonly TRAILESContext _context;

        public CabinsController(TRAILESContext context)
        {
            _context = context;
        }

        // GET: Cabins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cabin.ToListAsync());
        }

        // GET: Cabins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cabin == null)
            {
                return NotFound();
            }

            return View(cabin);
        }

        // GET: Cabins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cabins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Gender,BedCount,BedsFilled")] Cabin cabin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cabin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cabin);
        }

        // GET: Cabins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabin.FindAsync(id);
            if (cabin == null)
            {
                return NotFound();
            }
            return View(cabin);
        }

        // POST: Cabins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Gender,BedCount,BedsFilled")] Cabin cabin)
        {
            if (id != cabin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cabin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CabinExists(cabin.ID))
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
            return View(cabin);
        }

        // GET: Cabins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cabin == null)
            {
                return NotFound();
            }

            return View(cabin);
        }

        // POST: Cabins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cabin = await _context.Cabin.FindAsync(id);
            _context.Cabin.Remove(cabin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CabinExists(int id)
        {
            return _context.Cabin.Any(e => e.ID == id);
        }
    }
}
