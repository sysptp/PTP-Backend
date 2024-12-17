using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnScheduleService : GenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>, IGnScheduleService
    {
        public GnScheduleService(IGenericRepository<GnSchedule> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
