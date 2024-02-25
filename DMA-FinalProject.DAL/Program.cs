using DMA_FinalProject.DAL.DAO;
using DMA_FinalProject.DAL.Model;

namespace DMA_FinalProject.DAL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeDAO empDAO = new EmployeeDAO();
            Console.WriteLine(empDAO.Get("Employee1@gmail.com"));
        }
    }
}
