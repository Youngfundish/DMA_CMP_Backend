using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_FinalProject.DAL.Model
{
    public class UserConsent
    {
        public string UserId { get; set; }
        public string DomainUrl { get; set; }
        public bool Necessary { get; set; }
        public bool Functionality { get; set; }
        public bool Analytics { get; set; }
        public bool Marketing { get; set; }
    }
}
