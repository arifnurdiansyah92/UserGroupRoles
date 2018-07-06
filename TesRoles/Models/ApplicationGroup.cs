using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesRoles.Models
{
    public class ApplicationGroup
    {
        public int ApplicationGroupId { get; set; }
        public string akses { get; set; }
        public List<GroupRoles> GroupRoles;
    }
    public class GroupRoles
    {
        public int GroupRolesId { get; set; }
        public string roles { get; set; }

        public int ApplicationGroupId { get; set; }
        public ApplicationGroup ApplicationGroup { get; set; }
    }
}
        