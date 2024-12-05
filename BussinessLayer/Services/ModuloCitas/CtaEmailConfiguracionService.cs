using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaEmailConfiguracionService : GenericService<CtaEmailConfiguracionResponse, CtaEmailConfiguracionResponse, CtaEmailConfiguracion>, ICtaEmailConfiguracionService
    {
        public CtaEmailConfiguracionService(IGenericRepository<CtaEmailConfiguracion> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
