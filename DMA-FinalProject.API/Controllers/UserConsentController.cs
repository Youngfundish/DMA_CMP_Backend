using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DMA_FinalProject.API.Conversion;
using Microsoft.AspNetCore.Authorization;

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController, Authorize]
    public class UserConsentController : ControllerBase
    {
        private readonly UserConsentDAO dataAccess;

        public UserConsentController(UserConsentDAO dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        [Route("{browserId}/{domainUrl}")]
        public ActionResult<IEnumerable<UserConsentDTO>> Get(string browserId, string domainUrl)
        {
            return Ok(dataAccess.Get(browserId, domainUrl).UserConsentToDto());
        }

        [HttpPost]
        public ActionResult<bool> Add([FromBody] UserConsentDTO uc)
        {
            return Ok(dataAccess.Add(uc.UserConsentFromDto()));
        }

        [HttpPut]
        public ActionResult<bool> Update([FromBody] UserConsentDTO uc)
        {
            return Ok(dataAccess.Update(uc.UserConsentFromDto()));   
        }
    }
}
