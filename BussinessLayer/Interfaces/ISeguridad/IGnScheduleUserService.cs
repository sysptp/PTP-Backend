using BussinessLayer.DTOs.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnScheduleUserService : IGenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>
    {
    }
}
