using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BussinessLayer.DTOs.Paypal.CreateOrder
{
    public record AmountDto
    (
        [JsonProperty("currency_code")]
        string? Currency,
        [JsonProperty("value")]
        double Value
    )
    { }



}