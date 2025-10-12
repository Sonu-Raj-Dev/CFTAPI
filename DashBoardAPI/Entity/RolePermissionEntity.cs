namespace DashBoardAPI.Entity
{
    public class RolePermissionEntity:BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public string Name { get; set; }
    }
}
