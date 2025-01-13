using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mappings
{
    public class CmpContactoMappingProfile : Profile
    {
        public CmpContactoMappingProfile()
        {
            // Mapeo del modelo a DTO
            CreateMap<CmpContactos, CmpContactoDto>();

            // Mapeo de DTO de creación al modelo
            CreateMap<CmpContactoCreateDto, CmpContactos>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FechaModificacion, opt => opt.Ignore()) // Se gestiona automáticamente
                .ForMember(dest => dest.Borrado, opt => opt.MapFrom(src => false));

            // Mapeo de DTO de actualización al modelo
            CreateMap<CmpContactoUpdateDto, CmpContactos>()
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore()) // No debe modificarse en la actualización
                .ForMember(dest => dest.Borrado, opt => opt.Ignore()); // Se gestiona de forma independiente
        }
    }
}
