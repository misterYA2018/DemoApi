using Newtonsoft.Json;

namespace Tests.Models
{
    public class UserCreateResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
