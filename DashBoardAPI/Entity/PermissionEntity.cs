using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class PermissionEntity:BaseEntity
    {

        public Int64 Id { get; set; }
        public long PermissionId { get; set; }
        public string PermissionKey { get; set; } = "";
        public string Description { get; set; } = "";
        [JsonPropertyName("roleId")]
        public Int64 RoleId { get; set; }

        [JsonPropertyName("permissionIds")]
        public string PermissionIds { get; set; }
    }
}
