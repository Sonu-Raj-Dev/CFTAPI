using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAllComplaints()
        {
            var complaints = new List<ComplaintModel>
            {
                new()
                {
                    Id = 401,
                    CustomerId = 201,
                    CustomerName = "Acme Corp",
                    MobileNumber = "9000000000",
                    Email = "support@acme.com",
                    Address = "1 Infinite Loop",
                    NatureOfComplaint = "Installation",
                    Details = "Requires technician visit",
                    EngineerId = 301,
                    Status = "Open",
                    CreatedAt = DateTime.UtcNow
                }
            };

            return Ok(new ApiResponse<List<ComplaintModel>> { Success = true, Data = complaints });
        }

        [HttpPost("Create")]
        public IActionResult CreateComplaint([FromBody] CreateComplaintRequest request)
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Complaint created",
                Data = new { ComplaintId = 402 }
            });
        }

        [HttpPost("AssignEngineer")]
        public IActionResult AssignEngineer([FromBody] AssignEngineerRequest request)
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Engineer assigned"
            });
        }
    }
}
