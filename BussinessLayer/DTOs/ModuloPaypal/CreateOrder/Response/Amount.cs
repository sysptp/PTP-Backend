using Newtonsoft.Json;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record Amount(
    [JsonProperty("currency_code")] string CurrencyCode,
    [JsonProperty ("value")] string Value
);
}
