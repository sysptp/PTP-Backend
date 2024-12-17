using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnScheduleService : IGenericService<GnScheduleRequest, GnScheduleResponse, GnSchedule>
    {
    }
}
