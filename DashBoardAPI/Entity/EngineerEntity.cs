namespace DashBoardAPI.Entity
{
    public class EngineerEntity:BaseEntity
    {
        public long EngineerId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string MobileNumber { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
