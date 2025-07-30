using Newtonsoft.Json;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record Payee(
    [JsonProperty("email_address")] string EmailAddress,
    [JsonProperty("merchant_id")] string MerchantId,
    [JsonProperty("display_data")] DisplayData DisplayData
);
}
