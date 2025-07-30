using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Paypal.CreateOrder
{
    public record CreatePaypalOrderDto(

    [property: JsonPropertyName("intent")] string Intent,
    [property: JsonPropertyName("purchase_units")] List<PurchaseDetailDto> PurchaseUnits,
    [property: JsonPropertyName("application_context")] ApplicationContext ApplicationContext
)
    { }
}
