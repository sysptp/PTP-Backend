using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpClientes : Profile
    {
        public CmpClientes()
        {
            CreateMap<CmpClientCreateDto, CmpCliente>()
                .ReverseMap();
            
            CreateMap<CmpClienteUpdateDto, CmpCliente>()
                .ReverseMap();
            
            CreateMap<CmpClienteDto, CmpCliente>()
                .ReverseMap();
        }
    }
}
