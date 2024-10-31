using AutoMapper;
using DataLayer.Models.Entities;
using DataLayer.PDbContex;
using BussinessLayer.DTOs.Seguridad;
using Microsoft.EntityFrameworkCore;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Seguridad;

namespace BussinessLayer.Services.SSeguridad
{

    namespace BussinessLayer.Services.SOtros
    {
        public class GnPerfilService : GenericService<SaveGnPerfilDto, GnPerfilDto, GnPerfil>, IGnPerfilService
        {
            private readonly IGnPerfilRepository _gnPerfilRepository;
            private readonly IMapper _mapper;
            private readonly IClaimsService claimsService;

            public GnPerfilService(IGnPerfilRepository gnPerfilRepository, IMapper mapper, IClaimsService claimsService) : base(gnPerfilRepository, mapper)
            {
                _gnPerfilRepository = gnPerfilRepository;
                _mapper = mapper;
                this.claimsService = claimsService;
            }

            //public async Task<IList<GnPerfilDto>> GetAll(int? idPerfil = null, long? idEmpresa = null)
            //{
            //    //var query = _context.Set<GnPerfil>().AsQueryable();

            //    //if (idPerfil.HasValue)
            //    //{
            //    //    query = query.Where(p => p.IDPerfil == idPerfil.Value);
            //    //}

            //    //if (idEmpresa.HasValue)
            //    //{
            //    //    query = query.Where(p => p.IDEmpresa == idEmpresa.Value);
            //    //}

            //    //var perfiles = await query.ToListAsync();

            //    //return _mapper.Map<IList<GnPerfilDto>>(perfiles);
            //}

            //public async Task PatchUpdate(int id, Dictionary<string, object> updatedProperties)
            //{
            //    //var entity = await GET(id);
            //    //if (entity == null) throw new KeyNotFoundException("Role not found");

            //    //foreach (var property in updatedProperties)
            //    //{
            //    //    var propInfo = typeof(GnPerfil).GetProperty(property.Key);
            //    //    if (propInfo != null)
            //    //    {
            //    //        propInfo.SetValue(entity, property.Value);
            //    //    }
            //    //}

            //    //await _context.SaveChangesAsync();
            //}
        }
    }

}
