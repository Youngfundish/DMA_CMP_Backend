using DMA_FinalProject.API.Authentication;
using DMA_FinalProject.API.Conversion;
using DMA_FinalProject.API.DTO;
using DMA_FinalProject.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DMA_FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginDAO loginDAO;
        private readonly IConfiguration configuration;

        public LoginController(LoginDAO loginDAO, IConfiguration configuration)
        {
                this.loginDAO = loginDAO;
                this.configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult<EmployeeDTO> Login([FromBody] LoginDTO data)
        {
            string hashedPassword = loginDAO.GetHashByEmail(data.Email);
            if (BCryptTool.ValidatePassword(data.Password, hashedPassword))
            {
                var employee = loginDAO.Login(data.Email, data.Password).EmployeeToDto();
                employee.Token = CreateToken(data.Email);
                return Ok(employee);
            }
            else
            {
                return BadRequest("Not Found");
            }
        }


        private string CreateToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString())
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return token;
        }

        [HttpPost]
        [Route("checkToken")]
        public IActionResult CheckToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                return Unauthorized(false);
            }
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var _);
                if (claimsPrincipal.Identity.IsAuthenticated)
                {
                    // Token is valid
                    return Ok(true);
                }
                else
                {
                    return Unauthorized(false);
                }
            }
            catch (Exception)
            {
                return Unauthorized(false);
            }

        }
    }
}
