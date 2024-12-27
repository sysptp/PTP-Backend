using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaEmailConfiguracionService : GenericService<CtaEmailConfiguracionRequest, CtaEmailConfiguracionResponse, CtaEmailConfiguracion>, ICtaEmailConfiguracionService
    {
        public CtaEmailConfiguracionService(IGenericRepository<CtaEmailConfiguracion> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
