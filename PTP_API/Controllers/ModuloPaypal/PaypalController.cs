using BussinessLayer.DTOs.ModuloPaypal;
using BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response;
using BussinessLayer.Interfaces.Services.ModuloPaypal;
using BussinessLayer.Settings;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloPaypal
{
    [SwaggerTag("Endpoint para Paypal")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PaypalController : ControllerBase
    {
        private readonly IPaypalService paypalService;
        private readonly IConfiguration configuration;
        private readonly IValidator<PaypalOrderCreateDto> validator;

        public PaypalController(IPaypalService paypalService, IConfiguration configuration, IValidator<PaypalOrderCreateDto> validator)
        {
            this.paypalService = paypalService;
            this.configuration = configuration;
            this.validator = validator;
        }

        [HttpPost("create-order")]
        [ProducesResponseType(typeof(Response<PayPalOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<PayPalOrderResponse>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear order de pago", Description = "Endpoint para la creacion de una order de pago en paypal")]
        public async Task<IActionResult> CreateOrder(PaypalOrderCreateDto paypalOrderCreate)
        {

            var validationResult = await validator.ValidateAsync(paypalOrderCreate);

            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(Response<PayPalOrderResponse>.BadRequest(errors));
            }

            var result = await paypalService.CreatePayPalOrderAsync(paypalOrderCreate);


            if (result.Succeeded)
            {
                return Created("Order was created", result);
            }

            return BadRequest(result);
        }
    }
}
