using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesRoles.Data;
using TesRoles.Models;
using TesRoles.Services;


namespace TesRoles.Controllers
{
    public class GroupRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRole _role;

        public GroupRolesController(ApplicationDbContext context, IRole role)
        {
            _role = role;
            _context = context;
        }

        // GET: GroupRoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GroupRoles.Include(g => g.ApplicationGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GroupRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRoles = await _context.GroupRoles
                .Include(g => g.ApplicationGroup)
                .SingleOrDefaultAsync(m => m.GroupRolesId == id);
            if (groupRoles == null)
            {
                return NotFound();
            }

            return View(groupRoles);
        }

        // GET: GroupRoles/Create
        public IActionResult Create()
        {
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId");
            return View();
        }

        // POST: GroupRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupRolesId,roles,ApplicationGroupId")] GroupRoles groupRoles)
        {
            if (ModelState.IsValid)
            {
                groupRoles.roles = groupRoles.roles.ToLower();
                _context.Add(groupRoles);
                await _context.SaveChangesAsync();
                await _role.UpdateRole(groupRoles.ApplicationGroupId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId", groupRoles.ApplicationGroupId);
            return View(groupRoles);
        }

        // GET: GroupRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRoles = await _context.GroupRoles.SingleOrDefaultAsync(m => m.GroupRolesId == id);
            if (groupRoles == null)
            {
                return NotFound();
            }
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId", groupRoles.ApplicationGroupId);
            return View(groupRoles);
        }

        // POST: GroupRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupRolesId,roles,ApplicationGroupId")] GroupRoles groupRoles)
        {
            if (id != groupRoles.GroupRolesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    groupRoles.roles = groupRoles.roles.ToLower();
                    _context.Update(groupRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupRolesExists(groupRoles.GroupRolesId))
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
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId", groupRoles.ApplicationGroupId);
            return View(groupRoles);
        }

        // GET: GroupRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupRoles = await _context.GroupRoles
                .Include(g => g.ApplicationGroup)
                .SingleOrDefaultAsync(m => m.GroupRolesId == id);
            if (groupRoles == null)
            {
                return NotFound();
            }
            return View(groupRoles);
        }

        // POST: GroupRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupRoles = await _context.GroupRoles.SingleOrDefaultAsync(m => m.GroupRolesId == id);
            _context.GroupRoles.Remove(groupRoles);
            await _context.SaveChangesAsync();
            await _role.UpdateRole(groupRoles.ApplicationGroupId);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupRolesExists(int id)
        {
            return _context.GroupRoles.Any(e => e.GroupRolesId == id);
        }
    }
}
