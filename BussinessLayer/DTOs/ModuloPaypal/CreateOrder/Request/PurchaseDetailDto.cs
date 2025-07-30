using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BussinessLayer.DTOs.Paypal.CreateOrder
{
    public record PurchaseDetailDto(

        [JsonProperty("reference_id")] string ReferenceId,
        [JsonProperty("description")] string Description,
        [JsonProperty("amount")] AmountDto Amount
    )
    { }

}