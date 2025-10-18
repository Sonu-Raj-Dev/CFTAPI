using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMasterController : ControllerBase
    {
        [HttpPost("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = new List<UserModel>
            {
                new() { UserId = 101, Name = "Jane Doe", EmailId = "jane@example.com", MobileNumber = "9876543210", Address = "Baker St", IsActive = true }
            };

            return Ok(new ApiResponse<List<UserModel>> { Success = true, Data = users });
        }

        [HttpPost("GetUserRoles")]
        public IActionResult GetUserRoles([FromQuery] long userId)
        {
            return Ok(new ApiResponse<List<long>> { Success = true, Data = new List<long> { 1, 2 } });
        }  
    }
}
