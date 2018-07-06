using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesRoles.Data;
using TesRoles.Models;

namespace TesRoles.Controllers
{
    [Authorize(Roles = "revenue")]

    public class RevenueController : Controller
    {
 
        private readonly ApplicationDbContext _context;

        public RevenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Revenue
        public IActionResult Report()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Revenue.ToListAsync());
        }

        // GET: Revenue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenue
                .SingleOrDefaultAsync(m => m.RevenueId == id);
            if (revenue == null)
            {
                return NotFound();
            }

            return View(revenue);
        }

        // GET: Revenue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Revenue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevenueId,produk,total,bulan")] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revenue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revenue);
        }

        // GET: Revenue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenue.SingleOrDefaultAsync(m => m.RevenueId == id);
            if (revenue == null)
            {
                return NotFound();
            }
            return View(revenue);
        }

        // POST: Revenue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevenueId,produk,total,bulan")] Revenue revenue)
        {
            if (id != revenue.RevenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revenue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevenueExists(revenue.RevenueId))
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
            return View(revenue);
        }

        // GET: Revenue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenue
                .SingleOrDefaultAsync(m => m.RevenueId == id);
            if (revenue == null)
            {
                return NotFound();
            }

            return View(revenue);
        }

        // POST: Revenue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revenue = await _context.Revenue.SingleOrDefaultAsync(m => m.RevenueId == id);
            _context.Revenue.Remove(revenue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevenueExists(int id)
        {
            return _context.Revenue.Any(e => e.RevenueId == id);
        }
    }
}
