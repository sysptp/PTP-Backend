using AutoMapper;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interface.IAccount;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Repository.RSeguridad;
using BussinessLayer.Wrappers;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BussinessLayer.Services.SSeguridad
{
    public class GnPerfilService : GenericService<GnPerfilRequest,GnPerfilResponse,GnPerfil>,IGnPerfilService
    {
        private readonly IGnPerfilRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public GnPerfilService(IGnPerfilRepository repository, IMapper mapper, IRoleService roleService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<GnPerfilRequest> AddTest(GnPerfilRequest vm)
        {
            try
            {
                //var add = base.Add(vm);
                //if (add != null)
                //{
                    await _roleService.CreateRoleAsync(vm.Perfil, vm.Descripcion, vm.IDEmpresa);
                return new GnPerfilRequest();
                //}
                //return add;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

    }
}
