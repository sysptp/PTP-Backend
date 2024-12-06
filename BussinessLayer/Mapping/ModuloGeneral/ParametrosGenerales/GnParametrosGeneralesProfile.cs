using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using DataLayer.Models.ModuloGeneral;


namespace BussinessLayer.Mapping.ModuloGeneral.ParametrosGenerales
{
    public class GnParametrosGeneralesProfile:Profile
    {
        public GnParametrosGeneralesProfile()
        {

            CreateMap<GnParametrosGeneralesRequest, GnParametrosGenerales>().ReverseMap();

            CreateMap<GnParametrosGeneralesReponse, GnParametrosGenerales>().ReverseMap();

        }
    }
}
