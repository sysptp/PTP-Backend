using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnScheduleUserService : IGenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>
    {
        Task<List<GnScheduleUserResponse>> GetAllByFilters(long? companyId, int? userId, int? scheduleId);
    }
}
