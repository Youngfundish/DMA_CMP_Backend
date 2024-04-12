using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_FinalProject.DAL.DAO
{
    public static class DBConnection
    {
        public static string ConnectionString
        {
            get { return "Data Source = someDB; User ID = 123; Password = Password1!; Encrypt = False; TrustServerCertificate = True"; }
        }
    }
}
