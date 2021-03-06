using System.Text.Json.Serialization;

namespace web_services_ielectric.Security.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}