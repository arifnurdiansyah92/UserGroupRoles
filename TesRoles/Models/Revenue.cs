using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesRoles.Models
{
    public class Revenue
    {
        public int RevenueId { get; set; }
        public string produk { get; set; }
        public decimal total { get; set; }
        public string bulan { get; set; }
    }
}
