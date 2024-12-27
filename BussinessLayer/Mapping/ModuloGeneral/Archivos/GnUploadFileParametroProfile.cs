using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using DataLayer.Models.ModuloGeneral.Archivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping.ModuloGeneral.Archivos
{
    public class GnUploadFileParametroProfile : Profile
    {
        public GnUploadFileParametroProfile()
        {
            CreateMap<GnUploadFileParametro, CreateGnUploadFileParametroDto>().ReverseMap();
            CreateMap<GnUploadFileParametro, EditGnUploadFileParametroDto>().ReverseMap();
            CreateMap<GnUploadFileParametro, ViewGnUploadFileParametroDto>().ReverseMap();
        }
    }

}
