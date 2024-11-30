using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.Schedule
{
    public class GnScheduleUserService : GenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>, IGnScheduleUserService
    {
        public GnScheduleUserService(IGenericRepository<GnScheduleUser> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
