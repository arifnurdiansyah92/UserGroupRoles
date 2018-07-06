using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesRoles.Data;
using TesRoles.Models;

namespace TesRoles.Services
{
    public class Role : IRole
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Role(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task ChangeRole(ApplicationUser appUser, int role)
        {

            var appGroup = _context.ApplicationGroup.SingleOrDefault(m => m.ApplicationGroupId == role);
            if (appGroup != null)
            {
                appGroup.GroupRoles = _context.GroupRoles.Where(x => x.ApplicationGroupId == role).ToList();

                var currentRoles = await _userManager.GetRolesAsync(appUser);
                if (currentRoles != null)
                {
                    foreach(var item in currentRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, item);
                    }
                }

                foreach (var item in appGroup.GroupRoles)
                {
                    if(!await _roleManager.RoleExistsAsync(item.roles))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(item.roles));
                    }
                    await _userManager.AddToRoleAsync(appUser,item.roles);
                }
            }
            await _signInManager.RefreshSignInAsync(appUser);
        }
        public async Task UpdateRole(int id)
        {
            var appGroup = _context.ApplicationGroup.SingleOrDefault(m => m.ApplicationGroupId == id);
            if (appGroup != null)
            {
                var userList = _context.ApplicationUser.Where(x => x.ApplicationGroupId == id).ToList(); 
                appGroup.GroupRoles = _context.GroupRoles.Where(x => x.ApplicationGroupId == id).ToList();

                foreach (var item in appGroup.GroupRoles)
                {
                    if (!await _roleManager.RoleExistsAsync(item.roles))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(item.roles));
                    }
                }
                if(userList != null)
                {
                    foreach(var user in userList)
                    {
                        await ChangeRole(user, id);
                    }
                }


            }
        }
    }
}
