using DashBoardAPI.Entity;
using DashBoardAPI.Service.LoginService;
using DashBoardAPI.Service.RoleService;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleMasterController : ControllerBase
    {

        private readonly IRoleService _roleservice;


        public RoleMasterController(IRoleService roleservice)
        {

            this._roleservice = roleservice;
        }


        [HttpPost("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var data = _roleservice.GetRoles();

                if (data == null || !data.Any())
                {
                    return Ok(new ApiResponse<List<RoleEntity>>
                    {
                        Success = true,
                        Message = "No roles found",
                        Data = new List<RoleEntity>()
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Roles fetched sucessfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new ApiResponse<List<RoleEntity>>
                {
                    Success = false,
                    Message = "An error occurred while retrieving roles",
                    Data = null
                });
            }
        }

        [HttpPost("GetRolePermissions")]
        public IActionResult GetRolePermissions([FromQuery] long roleId)
        {
            return Ok(new ApiResponse<List<long>> { Success = true, Data = new List<long> { 10, 11, 12 } });
        }
    }
}
