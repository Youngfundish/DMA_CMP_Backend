using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_FinalProject.DAL.Model
{
    public class Cookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DomainURL { get; set; }
        public string Category { get; set; }
    }
}
