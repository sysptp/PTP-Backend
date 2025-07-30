using Newtonsoft.Json;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record PurchaseUnit(
    [JsonProperty("reference_id")] string ReferenceId,
    [JsonProperty("amount")] Amount Amount,
    [JsonProperty("payee")] Payee Payee,
    [JsonProperty("description")] string Description
);
}
