using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;

namespace LogProxyAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDTO model)
        {
            var user = await _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect"});

            return Ok(true);
        }        
    }
}
