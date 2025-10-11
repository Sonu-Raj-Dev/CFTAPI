using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleMasterController : ControllerBase
    {
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = new List<RoleModel>
            {
                new() { RoleId = 1, RoleName = "Admin", IsActive = true },
                new() { RoleId = 2, RoleName = "Engineer", IsActive = true }
            };

            return Ok(new ApiResponse<List<RoleModel>> { Success = true, Data = roles });
        }

        [HttpGet("GetRolePermissions")]
        public IActionResult GetRolePermissions([FromQuery] long roleId)
        {
            return Ok(new ApiResponse<List<long>> { Success = true, Data = new List<long> { 10, 11, 12 } });
        }
    }
}
