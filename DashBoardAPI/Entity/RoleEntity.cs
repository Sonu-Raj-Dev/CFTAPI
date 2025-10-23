using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class RoleEntity:BaseEntity
    {
        [JsonPropertyName("id")]
        public Int64 Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
