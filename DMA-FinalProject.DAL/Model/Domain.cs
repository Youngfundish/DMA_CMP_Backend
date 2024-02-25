using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_FinalProject.DAL.Model
{
    public class Domain
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int CookieAmount { get; set; } = 0;
    }
}
