using DashBoardAPI.Entity;
using DashBoardAPI.Service.DashBoardService;
using DashBoardAPI.Service.LoginService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginservice;


        public LoginController(ILoginService loginservice)
        {

            this._loginservice = loginservice;
        }


        [HttpPost("UserLogin")]
        public IActionResult UserLogin([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<LoginEntity>(request.GetRawText());

            if (data == null || string.IsNullOrWhiteSpace(data.Email) || string.IsNullOrWhiteSpace(data.Password))
            {
                return BadRequest(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Invalid request payload",
                    Data = null,
                    TotalCount = 0
                });
            }

            var userResponse = _loginservice.GetUserDetailsByEmailAndPassword(data.Email, data.Password);

             if (userResponse.Status == ApiStatus.OK)
                return Ok(userResponse);

            if (userResponse.Status == ApiStatus.AccessDenied)
                return Unauthorized(userResponse);

            return StatusCode(StatusCodes.Status500InternalServerError, userResponse);
        }

    }


}
