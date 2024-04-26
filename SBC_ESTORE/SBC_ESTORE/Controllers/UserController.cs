using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Services.ProductServices;
using SBC_ESTORE.Services.UserService;
using SBC_ESTORE.Shared.DTO.Product;
using SBC_ESTORE.Shared.DTO.User;
using System.Net;
using System.Security.Claims;

namespace SBC_ESTORE.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        IHttpContextAccessor contextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            this.userService = userService;
            this.contextAccessor = contextAccessor;
        }

        private ClaimsPrincipal? GetUserId() => contextAccessor.HttpContext?.User;

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserInfoForSideBar(int id)
        {
            var response = await userService.GetSideBarUserInfo(id);
            var User =  GetUserId();
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

        [HttpGet("details/{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails(int id)
        {
            var response = await userService.GetUserDetails(id);

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

        [HttpPut("details/{id}")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails(UserDetailsDTO user, int id)
        {
            var response = await userService.UpdateUserDetails(user, id);

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

        [HttpPut("change-password/{id}")]
        public async Task<ActionResult> UpdateUserPassword(ChangePasswordDTO password, int id)
        {
            var response = await userService.UpdatePassword(password, id);

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

        //Admin
        [HttpGet("admin")]
        public async Task<ActionResult<List<UserDetailsDTO>>> GetAllUserController()
        {
            var response = await userService.GetAllUsers();

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
    }
}
