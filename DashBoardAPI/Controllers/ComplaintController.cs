using DashBoardAPI.Entity;
using DashBoardAPI.Service.ComplaintService;
using DashBoardAPI.Service.EngineerService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintservice;


        public ComplaintController(IComplaintService complaintservice)
        {

            this._complaintservice = complaintservice;
        }
        [HttpPost("GetAllComplaints")]
        public IActionResult GetAllComplaints([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<ComplaintEntity>(request.GetRawText());


            var ComplaintDetails = _complaintservice.GetComplaintDetailsByUserIdAndRoleId(Convert.ToInt64(data.UserId),Convert.ToInt64(data.RoleId));

            return Ok(new
            {
                Success = true,
                Data = ComplaintDetails
            });
        }

        [HttpPost("Create")]
        public IActionResult CreateComplaint([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<ComplaintEntity>(request.GetRawText());

            var data1=_complaintservice.InsertComplaintDetails(data);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Complaint created",
                Data = new { ComplaintId = data1 }
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
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<ComplaintEntity>(request.GetRawText());

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Complaint deleted"
            });
        }
    }
}
