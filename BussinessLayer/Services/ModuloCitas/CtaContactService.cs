using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.DTOs.ModuloCitas.CtaContacts;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaContactService : GenericService<CtaContactRequest, CtaContactResponse, CtaContacts>, ICtaContactService
    {
        private readonly IMapper _mapper;
        private readonly ICtaContactRepository _contactRepository;

        public CtaContactService(IMapper mapper, ICtaContactRepository contactRepository) : base(contactRepository, mapper)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public override async Task<List<CtaContactResponse>> GetAllDto()
        { 
            var contactsList = await _contactRepository.GetAllWithIncludeAsync(new List<string> { "ContactType" });
            return _mapper.Map<List<CtaContactResponse>>(contactsList);

    }
    }
}
