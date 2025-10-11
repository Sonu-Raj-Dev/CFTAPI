using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleMaster1Controller : Controller
    {
        [HttpGet("GetRolePermissions")]
        public IActionResult GetRolePermissions()
        {
            // Static example: each role has tabs they can access
            var roles = new List<RolePermission>
            {
                new RolePermission
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    Permissions = new List<string> { "dashboard", "complaints", "registercomplaint", "customers" }
                },
                new RolePermission
                {
                    RoleId = 2,
                    RoleName = "User",
                    Permissions = new List<string> { "Dashboard", "Reports" }
                },
                new RolePermission
                {
                    RoleId = 3,
                    RoleName = "Manager",
                    Permissions = new List<string> { "Dashboard", "Reports", "Team" }
                }
            };

            var response = new RolePermissionResponse
            {
                Success = true,
                Message = "Role tab permissions fetched successfully",
                Data = roles
            };

            return Ok(response);
        }
    }

    public class RolePermission
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }  // here, "permissions" = tabs
    }

    public class RolePermissionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<RolePermission> Data { get; set; }
    }
}
