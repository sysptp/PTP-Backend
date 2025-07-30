using BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response;
using BussinessLayer.DTOs.ModuloPaypal;
using BussinessLayer.Interfaces.Services.ModuloPaypal;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using BussinessLayer.Helpers.UtilsHelpers;
using BussinessLayer.Wrappers;

public class PaypalService : IPaypalService
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClient;

    public PaypalService(IConfiguration config, IHttpClientFactory httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<Response<PayPalOrderResponse>> CreatePayPalOrderAsync(PaypalOrderCreateDto paypalOrderDto)
    {
        try
        {
            var paypalRequest = new
            {
                intent = ReadFromConfiguration.GetValueFromConfig(_config,"INTENT"),
                purchase_units = new[]
                {
                        new
                        {
                            reference_id = "ORDER-123",
                            description = paypalOrderDto.Description,
                            amount = new
                            {
                                currency_code = ReadFromConfiguration.GetValueFromConfig(_config,"CURRENCY_CODE"),
                                value = paypalOrderDto.Amount.ToString("F2")
                            }
                        }
                    },
                application_context = new
                {
                    brand_name = ReadFromConfiguration.GetValueFromConfig(_config, "BRAND_NAME"),
                    landing_page = "BILLING",
                    user_action = ReadFromConfiguration.GetValueFromConfig(_config, "USER_ACTION"),
                    return_url = ReadFromConfiguration.GetValueFromConfig(_config, "RETURN_URL"),
                    cancel_url = ReadFromConfiguration.GetValueFromConfig(_config, "CANCEL_URL"),
                    locale = "es-ES"
                }
            };

            string jsonPayload = JsonSerializer.Serialize(paypalRequest, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            var client = _httpClient.CreateClient("paypal_service");
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("",content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var paypalResponse = JsonSerializer.Deserialize<PayPalOrderResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                });

                return Response<PayPalOrderResponse>.Created(paypalResponse);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                return Response<PayPalOrderResponse>.BadRequest(new List<string> { errorContent });

            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error en CreatePayPalOrderAsync: {ex.Message}", ex);
        }
    }
}
