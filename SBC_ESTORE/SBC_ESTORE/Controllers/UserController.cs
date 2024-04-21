using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Services.UserService;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserInfoForSideBar(int id)
        {
            var response = await userService.GetSideBarUserInfo(id);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Data);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }


        [HttpPost("uploadAvatar/{id}")]
        public async Task<ActionResult> UploadUserAvatar(string AvatarUrl, int id)
        {
            var response = await userService.UploadAvatar(id, AvatarUrl);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }
    }
}
