using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaAppointmentUsersRepository : IGenericRepository<CtaAppointmentUsers>
    {
        Task<List<Usuario>> GetAllUserByAppointmentId(int appointmentId);
        Task DeleteByAppointmentId(int appointmentId, int userId);
    }
}
