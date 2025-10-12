namespace DashBoardAPI.Entity
{
    public class ComplaintEntity:BaseEntity
    {
        public long CustomerId { get; set; }
        public string NatureOfComplaint { get; set; } = "";
        public string Complaintdetails { get; set; } = "";
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public string EngineerName { get; set; }
        public string StatusName {  get; set; }
        public string UserId { get; set; }
        public string RoleId { get;set; }
    }
}
