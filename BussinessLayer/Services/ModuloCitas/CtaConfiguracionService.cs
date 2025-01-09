using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaConfiguracionService : GenericService<CtaConfiguracionRequest, CtaConfiguracionResponse, CtaConfiguration>, ICtaConfiguracionService
    {
        public CtaConfiguracionService(IGenericRepository<CtaConfiguration> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
