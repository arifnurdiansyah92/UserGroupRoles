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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TesRoles.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRole _role;

        public ApplicationUserController(IRole role,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _role = role;
        }

        // GET: ApplicationUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: ApplicationUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUser/Create
        public IActionResult Create()
        {
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId");

            return View();
        }

        // POST: ApplicationUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationGroupId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                Email = applicationUser.Email,
                UserName = applicationUser.Email,
                ApplicationGroupId = applicationUser.ApplicationGroupId
            };
            await _userManager.CreateAsync(appUser, applicationUser.PasswordHash);
            await _role.ChangeRole(appUser, appUser.ApplicationGroupId);
            return RedirectToAction(nameof(Index));
        }

        // GET: ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ApplicationGroupId"] = new SelectList(_context.ApplicationGroup, "ApplicationGroupId", "ApplicationGroupId");

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApplicationGroupId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser thisUser = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUser.Id)).FirstOrDefault();
                    thisUser.ApplicationGroupId= applicationUser.ApplicationGroupId;
                    thisUser.Email = applicationUser.Email;
                    thisUser.UserName = applicationUser.UserName;
                    thisUser.ApplicationGroupId = applicationUser.ApplicationGroupId;
                    List<String> roles = new List<string>
                    {
                        Data.Roles.Admin.Data,
                        Data.Roles.Member.Data
                    };
                    IList<String> userRoles = await _userManager.GetRolesAsync(thisUser);

                    //foreach (var item in roles)
                    //{
                    //    if (!await _roleManager.RoleExistsAsync(item))
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole(item));
                    //    }
                    //    if (thisUser.Akses == item)
                    //    {
                    //        if (!userRoles.Contains(item))
                    //        {
                    //            await _userManager.AddToRoleAsync(thisUser, item);
                    //        }
                    //    }
                    //}
                    //if (thisUser.Akses != Data.Roles.Admin.Data)
                    //{
                    //    await _userManager.RemoveFromRoleAsync(thisUser, Roles.Admin.Data);
                    //}

                    //if (thisUser.Akses != Data.Roles.Member.Data)
                    //{
                    //    await _userManager.RemoveFromRoleAsync(thisUser, Roles.Member.Data);
                    //}
                    await _role.ChangeRole(thisUser,thisUser.ApplicationGroupId);
                    _context.Update(thisUser);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(thisUser, false);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        // GET: ApplicationUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
