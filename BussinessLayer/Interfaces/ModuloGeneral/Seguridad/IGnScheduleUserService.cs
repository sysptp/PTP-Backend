using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnScheduleUserService : IGenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>
    {
    }
}
