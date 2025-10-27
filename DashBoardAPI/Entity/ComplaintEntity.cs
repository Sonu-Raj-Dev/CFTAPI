using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class ComplaintEntity:BaseEntity
    {
       [JsonPropertyName("id")]
        public Int64 Id { get; set; }
        public long CustomerId { get; set; }
        public string NatureOfComplaint { get; set; } = "";
        public string Complaintdetails { get; set; } = "";
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public string AssignedEngineer { get; set; }
        public string StatusName {  get; set; }
        public string UserId { get; set; }
        public string RoleId { get;set; }

        [JsonPropertyName("engineerId")]
        public Int64 EngineerId { get; set; }

        [JsonPropertyName("statusId")]
        public Int64 StatusId { get; set; }

        public DateTime AssignmentDate { get; set; }

    }
}
