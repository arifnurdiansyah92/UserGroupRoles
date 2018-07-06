
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesRoles.Models;

namespace TesRoles.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.ApplicationGroup.Any())
            {
                return; //Kalo udah ada seed nya ga akan tumbuh
            }

            ApplicationGroup admingroup = new ApplicationGroup() {akses="admin"};
            ApplicationGroup membergroup = new ApplicationGroup() { akses = "member" };
            context.ApplicationGroup.Add(admingroup);
            context.ApplicationGroup.Add(membergroup);
            context.SaveChanges();

            GroupRoles admin = new GroupRoles() { roles = "admin", ApplicationGroupId = admingroup.ApplicationGroupId };
            GroupRoles member = new GroupRoles() { roles = "member", ApplicationGroupId = membergroup.ApplicationGroupId };
            context.GroupRoles.Add(admin);
            context.GroupRoles.Add(member);
            context.SaveChanges();
        }
    }
}
