
using Newtonsoft.Json;

namespace BussinessLayer.DTOs.Account.Providers
{
    public class GoogleUserResponse
    {
        [JsonProperty("sub")]         
        public string? Sub { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("given_name")]  
        public string? Given_name { get; set; }

        [JsonProperty("family_name")]  
        public string? Family_name { get; set; }

        [JsonProperty("picture")]       
        public string? Picture { get; set; }

        [JsonProperty("email_verified")]
        public bool Email_verified { get; set; }

        [JsonProperty("aud")]          
        public string? Aud { get; set; }
    }
}
