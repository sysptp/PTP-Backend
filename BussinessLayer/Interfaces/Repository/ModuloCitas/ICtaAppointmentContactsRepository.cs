using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaAppointmentContactsRepository : IGenericRepository<CtaAppointmentContacts>
    {
        Task<List<CtaContacts>> GetAllContactsByAppointmentId(int appointmentId);
        Task DeleteByAppointmentId(int appointmentId, int contactId);
    }
}
