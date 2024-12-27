using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad
{
    public interface IGnScheduleUserService : IGenericService<GnScheduleUserRequest, GnScheduleUserResponse, GnScheduleUser>
    {
        Task<List<GnScheduleUserResponse>> GetAllByFilters(long? companyId, int? userId, int? scheduleId);
    }
}
