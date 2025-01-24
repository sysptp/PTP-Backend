using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCampana;
using DataLayer.Models.ModuloCampaña;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpCampanaProfile : Profile
    {
        public CmpCampanaProfile()
        {
            CreateMap<CmpCampanaCreateDto, CmpCampana>()
                .ReverseMap();
        }
    }
}
