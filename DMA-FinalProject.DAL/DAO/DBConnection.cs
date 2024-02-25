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
            get { return "Data Source = hildur.ucn.dk; User ID = DMA-CSD-S212_10407505; Password = Password1!; Encrypt = False; TrustServerCertificate = True"; }
        }
    }
}
