using System.Text.Json.Serialization;

namespace DashBoardAPI.Entity
{
    public class CustomerEntity:BaseEntity
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = "";
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
        [JsonPropertyName("address")]
        public string Address { get; set; } = "";
    }
}
