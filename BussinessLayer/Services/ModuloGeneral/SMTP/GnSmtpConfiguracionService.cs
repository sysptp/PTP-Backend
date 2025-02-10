using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Smtp;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.IModuloGeneral.SMTP;
using DataLayer.Models.ModuloGeneral.SMTP;

namespace BussinessLayer.Services.ModuloGeneral.SMTP
{
    public class GnSmtpConfiguracionService : GenericService<GnSmtpConfiguracionRequest, GnSmtpConfiguracionResponse, GnSmtpConfiguracion>, IGnSmtpConfiguracionService
    {
        public GnSmtpConfiguracionService(IGenericRepository<GnSmtpConfiguracion> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
