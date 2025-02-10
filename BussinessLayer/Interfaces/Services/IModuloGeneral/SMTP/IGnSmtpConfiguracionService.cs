
using BussinessLayer.DTOs.ModuloGeneral.Smtp;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.SMTP;

namespace BussinessLayer.Interfaces.Services.IModuloGeneral.SMTP
{
    public interface IGnSmtpConfiguracionService : IGenericService<GnSmtpConfiguracionRequest, GnSmtpConfiguracionResponse, GnSmtpConfiguracion>
    {
    }
}
