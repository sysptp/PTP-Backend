using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Schedule;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repositories;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnScheduleService : GenericService<GnScheduleRequest, GnScheduleResponse, GnScheduleResponse>, IGnScheduleService
    {
        public GnScheduleService(IGenericRepository<GnScheduleResponse> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
