using AutoMapper;
using BussinessLayer.DTOs.Contactos.ClienteContacto;
using BussinessLayer.DTOs.Contactos.TypeContact;
using DataLayer.Models.Contactos;

namespace BussinessLayer.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<TypeContact, TypeContactDto>()
                .ReverseMap()
                .ForMember(x => x.ClientContacts, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ForMember(x => x.DateUpdated, opt => opt.Ignore())
                .ForMember(x => x.DateAdded, opt => opt.Ignore())
                .ForMember(x => x.AddedBy, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<TypeContactRequest, TypeContact>()
                .ForMember(x => x.ClientContacts, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ClientContactDto, ClientContact>()
                 .ReverseMap();
        }
    }
}
