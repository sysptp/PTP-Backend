using Newtonsoft.Json;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record PayPalOrderResponse(
    [JsonProperty("id")] string Id,
    [JsonProperty("intent")] string Intent,
    [JsonProperty("status")] string Status,
    [JsonProperty("purchase_units")] List<PurchaseUnit> PurchaseUnits,
    [JsonProperty("create_time")] DateTime CreateTime,
    [JsonProperty("links")] List<PayPalLink> Links
);
}
