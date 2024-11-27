using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Monedas;

public class MonedasProfile : Profile
{
    public MonedasProfile() {

        CreateMap<ViewCurrencyDTO, Moneda>().ReverseMap();
        CreateMap<CreateCurrencyDTO, Moneda>().ReverseMap();
        CreateMap<EditCurrencyDTO, Moneda>().ReverseMap();
    }
}

