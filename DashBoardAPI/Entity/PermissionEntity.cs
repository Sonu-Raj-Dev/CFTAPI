namespace DashBoardAPI.Entity
{
    public class PermissionEntity:BaseEntity
    {
        public long PermissionId { get; set; }
        public string PermissionKey { get; set; } = "";
        public string Description { get; set; } = "";
        public Int64 RoleId { get; set; }
    }
}
