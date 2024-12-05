using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnScheduleUserService : GenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>, IGnScheduleUserService
    {
        public GnScheduleUserService(IGenericRepository<GnScheduleUser> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
