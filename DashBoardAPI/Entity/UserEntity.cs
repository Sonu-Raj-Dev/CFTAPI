namespace DashBoardAPI.Entity
{
    public class UserEntity:BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
