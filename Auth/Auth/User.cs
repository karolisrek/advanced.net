using System.Text.Json.Serialization;

namespace Auth
{
    public enum RoleType
    {
        Buyer,
        Manager
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleType Role { get; set; }
    }
}