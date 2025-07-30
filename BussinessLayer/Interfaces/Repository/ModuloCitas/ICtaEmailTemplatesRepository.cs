
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaEmailTemplatesRepository : IGenericRepository<CtaEmailTemplates>
    {
        string ReplaceEmailTemplateValues(string templateBody, CtaAppointmentsRequest appointment, string recipientName = null);
    }
}
