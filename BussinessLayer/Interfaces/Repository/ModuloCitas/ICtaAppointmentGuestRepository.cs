using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaAppointmentGuestRepository : IGenericRepository<CtaAppointmentGuest>
    {
        Task<List<CtaGuest>> GetAllGuestByAppointmentId(int appointmentId);
    }
}
