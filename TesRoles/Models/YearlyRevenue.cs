using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesRoles.Models
{
    public class YearlyRevenue
    {
        public int YearlyRevenueId{ get; set;}
        public string year { get; set; }
        public decimal total { get; set; }
    }
}
