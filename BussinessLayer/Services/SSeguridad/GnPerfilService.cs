using AutoMapper;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Repository.RSeguridad;
using BussinessLayer.Wrappers;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SSeguridad
{
    public class GnPerfilService : GenericService<GnPerfilRequest,GnPerfilResponse,GnPerfil>,IGnPerfilService
    {
        private readonly IGnPerfilRepository _repository;
        private readonly IMapper _mapper;

        public GnPerfilService(IGnPerfilRepository repository, IMapper mapper) : base(repository, mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

   }
}
