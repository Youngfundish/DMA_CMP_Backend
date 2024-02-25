using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DMA_FinalProject.DAL.Model;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.API.Conversion;
using DMA_FinalProject.API.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController, Authorize]
    public class UserController : Controller
    {
        private readonly IDMAFinalProjectDAO<User> dataAccess;

        public UserController(IDMAFinalProjectDAO<User> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            return Ok(dataAccess.GetAll().UserToDtos());
        }

        [HttpGet]
        [Route("{browserId}")]
        public ActionResult<UserDTO> Get(string browserId)
        {
            UserDTO user = null;
            if(dataAccess.Get(browserId.ToLower()) != null){
                user = dataAccess.Get(browserId.ToLower()).UserToDto();
            }
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<bool> Add([FromBody] UserDTO u)
        {
            return Ok(dataAccess.Add(u.UserFromDto()));
        }

        [HttpDelete]
        [Route("{browserId}")]
        public ActionResult<bool> Delete(string browserId)
        {
            if (dataAccess.Remove(browserId.ToLower()))
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
