using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class EngineerEntity:BaseEntity
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        [JsonPropertyName("mobilenumber")]
        public string MobileNumber { get; set; } = "";
        [JsonPropertyName("isactive")]
        public bool IsActive { get; set; }
    }
}
