using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PiSec.Api.BusinessLogic.UserBSL;
using PiSec.Api.Entities;
using PiSec.Api.Extension;
using PiSec.Api.Model;
using PiSec.Api.Model.AppSettingModel;
using PiSec.Api.Model.RequestModel;
using PiSec.Api.Repository;
using System.Reflection;
using System.Xml.Linq;

namespace PiSec.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppSettingModel _setting;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserController(IOptions<AppSettingModel> setting ,ILogger<UserController> logger, AppDbContext context, IUserService userService)
        {
            _logger = logger;
            _setting = setting.Value;
            _context = context;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{name?}")]
        public IActionResult GetUsers(string name = null)
        {
            try
            {
                _logger.LogInformation("Start GetUsers with query name => {name}", name);

                var response = _userService.GetUsers(name);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(500, ex.ToString()));
            }            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel req)
        {
            try
            {
                _logger.LogInformation("Start CreateUser with request => {req}", req);

                var response = await _userService.CreateUser(req);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode,response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(500,ex.ToString()));
            }            
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestModel req, int id)
        {
            try
            {
                _logger.LogInformation("Start UpdateUser with request => {req}", req);

                 var response = await _userService.UpdateUsers(req,id);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(500,ex.ToString()));
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation("Start DeleteUser with request => {req}", id);

                var response = await _userService.DeleteUsers(id);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(500,ex.ToString()));
            }            
        }
    }
}
