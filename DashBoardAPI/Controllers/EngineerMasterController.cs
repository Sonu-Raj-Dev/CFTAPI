using DashBoardAPI.Entity;
using DashBoardAPI.Service.EngineerService;
using DashBoardAPI.Service.LoginService;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EngineerMasterController : ControllerBase
    {

        private readonly IEngineerService _engineerservice;


        public EngineerMasterController(IEngineerService engineerservice)
        {

            this._engineerservice = engineerservice;
        }
        [HttpPost("GetAllEngineers")]
        public IActionResult GetAllEngineers()
        {
           var Engineer=_engineerservice.GetEngineerDetails();

            return Ok(new
            {
                Success = true,
                Data = Engineer
            });
        }
    }
}
