using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaCitaConfiguracionService : GenericService<CtaCitaConfiguracionRequest, CtaCitaConfiguracionResponse, CtaCitaConfiguracion>, ICtaCitaConfiguracionService
    {
        public CtaCitaConfiguracionService(IGenericRepository<CtaCitaConfiguracion> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
