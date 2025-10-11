using DashBoardAPI.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DashBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginMasterController : ControllerBase
    {
        /// <summary>
        /// POST: /api/LoginMaster/GetLoginDetailsByEmailIdAndPassword
        /// AuthRequired: false
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Invalid request payload",
                    Data = null,
                    TotalCount = 0
                });
            }

            // Simulated authentication for demo
            if (request.Email == "user@example.com" && request.Password == "Secret#123")
            {
                var userData = new UserData
                {
                    Id = 101,
                    Name = "Jane Doe",
                    Email = request.Email,
                    Roles = new List<string> { "Admin" },
                    Permissions = new Dictionary<long, string>
                    {
                        { 1, "dashboard" },
                        { 2, "complaints" }
                    },
                    Token = "jwt-or-session-token",
                    RoleId = 1
                };

                return Ok(new JsonResponseEntity
                {
                    Status = ApiStatus.OK,
                    Message = "Login successful",
                    Data = userData,
                    TotalCount = 1
                });
            }

            return Unauthorized(new JsonResponseEntity
            {
                Status = ApiStatus.AccessDenied,
                Message = "Invalid credentials",
                Data = null,
                TotalCount = 0
            });
        }
    }

    // ✅ REQUEST SCHEMA
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // ✅ USER DATA (Nested object inside response)
    public class UserData
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public List<string> Roles { get; set; }
        public Dictionary<long, string> Permissions { get; set; }
        public string Token { get; set; }
        public long RoleId { get; set; }
    }
}
