using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using DataLayer.Models.ModuloCampaña;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Mapping.ModuloCampaña
{
    public class CmpServidoresSmtpProfile : Profile
    {
        public CmpServidoresSmtpProfile()
        {
            CreateMap<CmpServidoresSmtp, CmpServidoresSmtpDto>();
            CreateMap<CmpServidoresSmtpCreateDto, CmpServidoresSmtp>();
            CreateMap<CmpServidoresSmtpUpdateDto, CmpServidoresSmtp>();
        }
    }
}
