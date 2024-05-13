using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PiSec.Api.Model;
using PiSec.Api.Model.AppSettingModel;
using PiSec.Api.Model.RequestModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PiSec.Api.Controllers
{
    public class AuthenController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppSettingModel _setting;

        public AuthenController(IOptions<AppSettingModel> setting, ILogger<UserController> logger)
        {
            _logger = logger;
            _setting = setting.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel credentials)
        {
            if (credentials.Username == "admin" && credentials.Password == "P@ssw0rd")
            {
                var token = GenerateJwtToken();
                return Ok(new ResponseModel<string>(200,"User found successfully.", token));
            }
            return Unauthorized();
        }
        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.Jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_setting.Jwt.Issuer,
                _setting.Jwt.Audience,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
