using DashBoardAPI.Entity;
using DashBoardAPI.Service.ComplaintService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
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
        public IActionResult AssignEngineer([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<ComplaintEntity>(request.GetRawText());

           var data1= _complaintservice.AssignEngineerToComplaint(data);
            var complaintid = data.Id;

            #region Send Mail
            var emaildata = _complaintservice.GetEmailDetailByComplaintId(complaintid);

            if (emaildata != null)
            {
                var emailSent =  SendEmailToEngineer(emaildata.Data);
                //if (emailSent)
                //{
                //    // Optionally update complaint status or log email sent
                //   // _complaintservice.UpdateEmailStatus(complaintid, true);
                //}
            }
            #endregion
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Engineer assigned",
                Data= data1
            });
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] JsonElement request)
        {
            JsonResponseEntity apiResponse = new JsonResponseEntity();
            var data = JsonSerializer.Deserialize<ComplaintEntity>(request.GetRawText());
            _complaintservice.DeleteComplaint(data.Id);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Complaint deleted"
            });
        }
        private async Task<bool> SendEmailToEngineer(dynamic emaildata)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress((string)emaildata.EmailFrom, emaildata.UserName);
                    mail.To.Add((string)emaildata.EmailTo);
                    mail.Subject = (string)emaildata.Subject;
                    mail.Body = (string)emaildata.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        // Use the App Password here, not your regular Gmail password
                        smtp.Credentials = new NetworkCredential(
                            (string)emaildata.EmailFrom,
                            emaildata.Password
                        );
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        await smtp.SendMailAsync(mail); // Use async version
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                Console.WriteLine($"Full error: {ex}");
                return false;
            }
        }
    }
}
