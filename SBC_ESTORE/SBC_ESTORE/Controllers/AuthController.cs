using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Services.AuthServices;
using SBC_ESTORE.Shared.DTO.User;
using static SBC_ESTORE.Services.ServiceResponse.Response;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDTO user)
        {
            var response = await authService.CreateAccount(user);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO user)
        {
            var response = await authService.LoginAccount(user);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Token);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }
    }
}
