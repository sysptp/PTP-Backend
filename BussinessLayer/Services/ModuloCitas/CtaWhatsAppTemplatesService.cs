using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaWhatsAppTemplates;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaWhatsAppTemplatesService : GenericService<CtaWhatsAppTemplatesRequest, CtaWhatsAppTemplatesResponse, CtaWhatsAppTemplates>, ICtaWhatsAppTemplatesService
    {
        public CtaWhatsAppTemplatesService(IGenericRepository<CtaWhatsAppTemplates> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
