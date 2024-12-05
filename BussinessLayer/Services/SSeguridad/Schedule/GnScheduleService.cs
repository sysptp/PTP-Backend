using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.Schedule
{
    public class GnScheduleService : GenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>, IGnScheduleService
    {
        public GnScheduleService(IGenericRepository<GnSchedule> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
