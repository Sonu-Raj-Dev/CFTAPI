using DashBoardAPI.Entity;
using DashBoardAPI.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMasterController : ControllerBase
    {


        private readonly IUserService _userservice;


        public UserMasterController(IUserService userservice)
        {

            this._userservice = userservice;
        }
        [HttpPost("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var Engineer = _userservice.GetUserDetails();

            return Ok(new
            {
                Success = true,
                Data = Engineer
            });
        }
       

        [HttpPost("GetUserRoles")]
        public IActionResult GetUserRoles([FromQuery] long userId)
        {
            return Ok(new ApiResponse<List<long>> { Success = true, Data = new List<long> { 1, 2 } });
        }  
    }
}
