using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {

            _config = config;

        }
        private User AuthenticateUser(User user)
        {
            User _user = null;
            if(user.name=="admin" && user.password=="password")
            {
                _user=new User { name = "Anand" };
            }
            return _user;
        }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires:DateTime.Now.AddDays(1)
                ,signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult actionResult = Unauthorized();
            var _user=AuthenticateUser(user);
            if(_user!=null)
            {
                var token=GenerateToken(user);
                return Ok(JsonSerializer.Serialize<string>(token));
            }
            return actionResult;
        }
    }
}
