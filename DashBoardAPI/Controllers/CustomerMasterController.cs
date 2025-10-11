using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using static DashBoardAPI.Entity.MasterModels;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerMasterController : ControllerBase
    {
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var customers = new List<CustomerModel>
            {
                new() { CustomerId = 201, CustomerName = "Acme Corp", MobileNumber = "9000000000", Email = "support@acme.com", Address = "1 Infinite Loop" }
            };

            return Ok(new ApiResponse<List<CustomerModel>> { Success = true, Data = customers });
        }
    }
}
