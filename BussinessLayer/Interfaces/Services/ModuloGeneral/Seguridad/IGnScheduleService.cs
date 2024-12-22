using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad
{
    public interface IGnScheduleService : IGenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>
    {
    }
}
