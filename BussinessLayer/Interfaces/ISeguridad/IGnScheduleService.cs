using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnScheduleService : IGenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>
    {
    }
}
