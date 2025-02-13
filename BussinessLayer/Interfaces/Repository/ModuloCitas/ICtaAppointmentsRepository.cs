using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Repository.ModuloCitas
{
    public interface ICtaAppointmentsRepository : IGenericRepository<CtaAppointments>
    {
        Task<List<CtaAppointments>> GetAppointmentsByDate(DateTime date, long companyId);
    }
}
