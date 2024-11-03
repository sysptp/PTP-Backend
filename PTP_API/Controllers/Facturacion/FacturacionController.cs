using BussinessLayer.DTOs.Facturacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Facturacion
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FacturacionController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceDto createInvoiceDto)
        {
            return Ok();
        }
    }
}
