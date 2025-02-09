using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaContacts;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaContactService : GenericService<CtaContactRequest, CtaContactResponse, CtaContacts>, ICtaContactService
    {
        public CtaContactService(IGenericRepository<CtaContacts> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
