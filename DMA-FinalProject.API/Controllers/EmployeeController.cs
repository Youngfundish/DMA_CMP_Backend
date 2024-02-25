using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DMA_FinalProject.DAL.DAO;
using DMA_FinalProject.DAL.Model;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.API.Conversion;
using DMA_FinalProject.API.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController, Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IDMAFinalProjectDAO<Employee> dataAccess;

        public EmployeeController(IDMAFinalProjectDAO<Employee> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDTO>> GetAll()
        {
            return Ok(dataAccess.GetAll().EmployeeToDtos());
        }

        [HttpGet]
        [Route("{email}")]
        public ActionResult<EmployeeDTO> Get(string email)
        {
            var employee = dataAccess.Get(email.ToLower()).EmployeeToDto();
            if (employee == null) { return NotFound(); }

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<bool> Add([FromBody] EmployeeDTO e)
        {
            e.Email.ToLower();
            e.PasswordHash = BCryptTool.HashPassword(e.PasswordHash);
            return Ok(dataAccess.Add(e.EmployeeFromDto()));
        }

        [HttpPut]
        public ActionResult<bool> Update(EmployeeDTO e)
        {
            e.Email.ToLower();
            e.PasswordHash = BCryptTool.HashPassword(e.PasswordHash);
            return Ok(dataAccess.Update(e.EmployeeFromDto()));
        }

        [HttpDelete("{email}")]
        public ActionResult<bool> Delete(string email)
        {
            if (dataAccess.Remove(email.ToLower()))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
