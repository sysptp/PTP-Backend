using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Paypal.CreateOrder;

public record ApplicationContext
    (
    [JsonProperty("brand_name")] string BrandName,
    [JsonProperty("user_action")] string UserAction,
    [JsonProperty("return_url")] string ReturnUrl,
    [JsonProperty("cancel_url")] string CancelUrl
    )
{
}