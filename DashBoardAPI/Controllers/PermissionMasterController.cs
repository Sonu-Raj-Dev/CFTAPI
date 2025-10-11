using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionMasterController : ControllerBase
    {
        [HttpGet("GetAllPermissions")]
        public IActionResult GetAllPermissions()
        {
            var permissions = new List<PermissionModel>
            {
                new() { PermissionId = 10, PermissionKey = "Users.Read", Description = "Can read users" }
            };

            return Ok(new ApiResponse<List<PermissionModel>> { Success = true, Data = permissions });
        }
    }
}
