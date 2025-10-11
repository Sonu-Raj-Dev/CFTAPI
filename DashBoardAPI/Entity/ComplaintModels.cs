namespace DashBoardAPI.Entity
{
    public class ComplaintModel
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string MobileNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string NatureOfComplaint { get; set; } = "";
        public string Details { get; set; } = "";
        public long? AssignedEngineer { get; set; }
        public string Status { get; set; } = "";
        public string ComplaintDetails { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public Int64 EngineerId { get; set; }
    }

    public class CreateComplaintRequest
    {
        public long CustomerId { get; set; }
        public string NatureOfComplaint { get; set; } = "";
        public string Details { get; set; } = "";
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    public class AssignEngineerRequest
    {
        public long ComplaintId { get; set; }
        public long EngineerId { get; set; }
    }
}
