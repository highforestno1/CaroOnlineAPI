using System.Threading.Tasks;
using Lab01.RequestModels;
using Lab01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {

        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationUser request)
        {
            var response = await _userService.Registration(request);
            return Ok(response);
        }
        
    }
}