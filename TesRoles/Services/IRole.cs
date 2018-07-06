using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesRoles.Models;

namespace TesRoles.Services
{
    public interface IRole
    {
        Task ChangeRole(ApplicationUser appUser, int role);
        Task UpdateRole(int id);
    }
}
