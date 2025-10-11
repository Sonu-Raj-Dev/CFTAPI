using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EngineerMasterController : ControllerBase
    {
        [HttpGet("GetAllEngineers")]
        public IActionResult GetAllEngineers()
        {
            var engineers = new List<EngineerModel>
            {
                new() { EngineerId = 301, Name = "John Smith", Email = "john@cft.com", MobileNumber = "9555555555", IsActive = true }
            };

            return Ok(new ApiResponse<List<EngineerModel>> { Success = true, Data = engineers });
        }
    }
}
