using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Repository.Modulo_Citas
{
    public interface ICtaSessionDetailsRepository : IGenericRepository<CtaSessionDetails>
    {
        List<CtaSessionDetails> GetAllSessionDetailsBySessionId(int sessionId);
        Task<List<CtaAppointments>> GetAllAppointmentsBySessionId(int sessionId);
        Task<List<CtaSessionDetails>> GetAllBySessionId(int sessionId);
    }

}

