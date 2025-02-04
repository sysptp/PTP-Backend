using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.Account.Providers;
using BussinessLayer.Interfaces.Services.IOtros;
using Newtonsoft.Json;

namespace BussinessLayer.Services.Otros.AutenticationStrategy
{
    public class GoogleTokenVerificationStrategy : ITokenVerificationStrategy
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;

        public GoogleTokenVerificationStrategy(HttpClient httpClient, string clientId)
        {
            _httpClient = httpClient;
            _clientId = clientId;
        }

        public async Task<ExternalUserInfo> VerifyTokenAsync(string token)
        {
            var response = await _httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={token}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var googleUser = JsonConvert.DeserializeObject<GoogleUserResponse>(content);

            return new ExternalUserInfo
            {
                Email = googleUser.Email,
                FirstName = googleUser.Given_name,
                LastName = googleUser.Family_name,
                ProviderUserId = googleUser.Sub
            };
        }
    }

}
