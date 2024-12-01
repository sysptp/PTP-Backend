using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;

namespace BussinessLayer.Services.SSeguridad.Schedule
{
    public class GnScheduleService : GenericService<GnScheduleRequest, GnScheduleResponse, GnScheduleResponse>, IGnScheduleService
    {
        public GnScheduleService(IGenericRepository<GnScheduleResponse> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
