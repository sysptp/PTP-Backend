using BussinessLayer.DTOs.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnScheduleService : IGenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>
    {
    }
}
