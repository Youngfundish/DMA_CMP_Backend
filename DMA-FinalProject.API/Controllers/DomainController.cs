using DMA_FinalProject.API.Conversion;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController, Authorize]
    public class DomainController : ControllerBase
    {
        private readonly DomainDAO domainDAO;

        public DomainController(DomainDAO domainDAO)
        {
            this.domainDAO = domainDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DomainDTO>> GetAll()
        {
            return Ok(domainDAO.GetAll().DomainToDtos());
        }

        [HttpGet]
        [Route("{companyId}")]
        public ActionResult<IEnumerable<DomainDTO>> Get(int companyId)
        {
            return Ok(domainDAO.Get(companyId).DomainToDtos());
        }

        [HttpPost]
        public ActionResult<bool> Add([FromBody] DomainDTO d)
        {
            d.Url = d.Url.ToLower();
            return Ok(domainDAO.Add(d.DomainFromDto()));
        }

        [HttpPut]
        public ActionResult<bool> Update(DomainDTO d)
        {
            return Ok(domainDAO.Update(d.DomainFromDto()));
        }

        [HttpDelete("{url}")]
        public ActionResult<bool> Delete(string url)
        {
            if (domainDAO.Remove(url.ToLower()))
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
