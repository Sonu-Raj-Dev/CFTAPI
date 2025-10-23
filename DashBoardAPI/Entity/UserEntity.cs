using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class UserEntity:BaseEntity
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("userid")]
        public Int64 UserId { get; set; }
        [JsonPropertyName("roleid")]
        public Int64 RoleId { get; set; }
    }
}
