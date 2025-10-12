namespace DashBoardAPI.Entity
{
    public class CustomerEntity:BaseEntity
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string MobileNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
