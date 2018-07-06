using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesRoles.Data;
using TesRoles.Models;

namespace TesRoles.Controllers
{
    public class ApplicationGroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationGroup.ToListAsync());
        }

        // GET: ApplicationGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationGroup = await _context.ApplicationGroup
                .SingleOrDefaultAsync(m => m.ApplicationGroupId == id);
            if (applicationGroup == null)
            {
                return NotFound();
            }

            return View(applicationGroup);
        }

        // GET: ApplicationGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationGroupId,akses")] ApplicationGroup applicationGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationGroup);
        }

        // GET: ApplicationGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationGroup = await _context.ApplicationGroup.SingleOrDefaultAsync(m => m.ApplicationGroupId == id);
            if (applicationGroup == null)
            {
                return NotFound();
            }
            return View(applicationGroup);
        }

        // POST: ApplicationGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationGroupId,akses")] ApplicationGroup applicationGroup)
        {
            if (id != applicationGroup.ApplicationGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationGroupExists(applicationGroup.ApplicationGroupId))
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
            return View(applicationGroup);
        }

        // GET: ApplicationGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationGroup = await _context.ApplicationGroup
                .SingleOrDefaultAsync(m => m.ApplicationGroupId == id);
            if (applicationGroup == null)
            {
                return NotFound();
            }

            return View(applicationGroup);
        }

        // POST: ApplicationGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationGroup = await _context.ApplicationGroup.SingleOrDefaultAsync(m => m.ApplicationGroupId == id);
            _context.ApplicationGroup.Remove(applicationGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationGroupExists(int id)
        {
            return _context.ApplicationGroup.Any(e => e.ApplicationGroupId == id);
        }
    }
}
