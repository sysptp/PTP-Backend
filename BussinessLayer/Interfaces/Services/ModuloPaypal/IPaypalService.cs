using BussinessLayer.DTOs.ModuloPaypal;
using BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Interfaces.Services.ModuloPaypal
{
    public interface IPaypalService
    {
        Task<Response<PayPalOrderResponse>> CreatePayPalOrderAsync(PaypalOrderCreateDto paypalOrderDto);
    }
}
