using Newtonsoft.Json;

namespace TD2.Ressources
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}