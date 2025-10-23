using DashBoardAPI.Entity;
using DashBoardAPI.Service.ComplaintService;
using DashBoardAPI.Service.LoginService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionMasterController : ControllerBase
    {
        private readonly ILoginService _loginservice;


        public PermissionMasterController(ILoginService complaintservice)
        {

            this._loginservice = complaintservice;
        }

        [HttpGet("GetAllPermissionByUserId")]
        public IActionResult GetAllPermissionByUserId([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<PermissionEntity>(request.GetRawText());


            var Permissions = _loginservice.GetPermissionByUserId(data.Id);

            return Ok(new
            {
                Success = true,
                Data = Permissions
            });
        }
        [HttpGet("GetAllPermissions")]
        public IActionResult GetAllPermissions()
        {      

            var Permissions = _loginservice.GetAllPermissions();

            return Ok(new
            {
                Success = true,
                Data = Permissions
            });
        }
        [HttpGet("GetAllPermissionByRoleId")]
        public IActionResult GetAllPermissionByRoleId([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<PermissionEntity>(request.GetRawText());


            var Permissions = _loginservice.GetPermissionsByRoleId(data.RoleId);

            return Ok(new
            {
                Success = true,
                Data = Permissions
            });
        }
    }
}
