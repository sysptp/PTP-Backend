using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Repository.ModuloCitas
{
    public interface ICtaAppointmentsRepository : IGenericRepository<CtaAppointments>
    {
        Task<List<CtaAppointments>> GetAppointmentsByDate(DateTime date, long companyId, int userId);
        Task<List<CtaAppointments>> GetAppointmentsInRange(DateTime startDate, DateTime endDate, long companyId,int userId);
    }
}
