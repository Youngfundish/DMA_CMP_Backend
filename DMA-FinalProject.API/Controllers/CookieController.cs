using DMA_FinalProject.API.Conversion;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController, Authorize]
    public class CookieController : ControllerBase
    {
        private readonly CookieDAO cookieDAO;
        public CookieController(CookieDAO cookieDAO)
        {
            this.cookieDAO = cookieDAO;
        }
        // GET: api/<CookieController>
        [HttpGet]
        public ActionResult<IEnumerable<CookieDTO>> Get()
        {
            return Ok(cookieDAO.GetAll().CookieToDtos());
        }

        // GET api/<CookieController>/5
        [HttpGet("{domainURL}")]
        public ActionResult<IEnumerable<CookieDTO>> Get(string domainURL)
        {
            return Ok(cookieDAO.Get(domainURL).CookieToDtos());
        }

        // POST api/<CookieController>
        [HttpPost]
        public ActionResult<bool> Add([FromBody] IEnumerable<CookieDTO> cookieDTOs)
        {
            return Ok(cookieDAO.Add(cookieDTOs.CookieFromDtos()));
        }
    }
}
