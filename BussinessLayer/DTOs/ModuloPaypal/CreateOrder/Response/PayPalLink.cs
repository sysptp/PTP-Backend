using Newtonsoft.Json;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record PayPalLink(
    [JsonProperty("href")] string Href,
    [JsonProperty("rel")] string Rel,
    [JsonProperty("method")] string Method){ }
}

