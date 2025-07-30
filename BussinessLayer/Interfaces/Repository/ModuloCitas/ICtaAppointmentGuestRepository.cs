using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaAppointmentGuestRepository : IGenericRepository<CtaAppointmentGuest>
    {
        Task<List<CtaGuest>> GetAllGuestByAppointmentId(int appointmentId);
        Task DeleteByAppointmentId(int appointmentId, int guestId);
    }
}
