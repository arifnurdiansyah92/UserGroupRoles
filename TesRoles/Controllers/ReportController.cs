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
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles="report")]
        // GET: Report
        public async Task<IActionResult> Index()
        {
            return View(await _context.YearlyRevenue.ToListAsync());
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yearlyRevenue = await _context.YearlyRevenue
                .SingleOrDefaultAsync(m => m.YearlyRevenueId == id);
            if (yearlyRevenue == null)
            {
                return NotFound();
            }

            return View(yearlyRevenue);
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YearlyRevenueId,year,total")] YearlyRevenue yearlyRevenue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yearlyRevenue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yearlyRevenue);
        }

        // GET: Report/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yearlyRevenue = await _context.YearlyRevenue.SingleOrDefaultAsync(m => m.YearlyRevenueId == id);
            if (yearlyRevenue == null)
            {
                return NotFound();
            }
            return View(yearlyRevenue);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YearlyRevenueId,year,total")] YearlyRevenue yearlyRevenue)
        {
            if (id != yearlyRevenue.YearlyRevenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yearlyRevenue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YearlyRevenueExists(yearlyRevenue.YearlyRevenueId))
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
            return View(yearlyRevenue);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yearlyRevenue = await _context.YearlyRevenue
                .SingleOrDefaultAsync(m => m.YearlyRevenueId == id);
            if (yearlyRevenue == null)
            {
                return NotFound();
            }

            return View(yearlyRevenue);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yearlyRevenue = await _context.YearlyRevenue.SingleOrDefaultAsync(m => m.YearlyRevenueId == id);
            _context.YearlyRevenue.Remove(yearlyRevenue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YearlyRevenueExists(int id)
        {
            return _context.YearlyRevenue.Any(e => e.YearlyRevenueId == id);
        }
    }
}
