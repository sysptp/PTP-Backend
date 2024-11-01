using AutoMapper;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Wrappers;
using DataLayer.Models.Entities;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SSeguridad
{
    public class GnPerfilService : IGnPerfilService
    {
        private readonly IGnPerfilRepository _repository;
        private readonly IMapper _mapper;

        public GnPerfilService(IGnPerfilRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GnPerfilDto>>> GetAll(int? idPerfil, long? idEmpresa)
        {
            var query = _repository.Query();

            if (idPerfil.HasValue)
                query = query.Where(x => x.Id == idPerfil.Value);

            if (idEmpresa.HasValue)
                query = query.Where(x => x.IDEmpresa == idEmpresa.Value);

            var perfiles = await query.ToListAsync();

            if (!perfiles.Any())
                return Response<IEnumerable<GnPerfilDto>>.NoContent("No se encontraron perfiles.");

            var perfilDtos = _mapper.Map<IEnumerable<GnPerfilDto>>(perfiles);
            return Response<IEnumerable<GnPerfilDto>>.Success(perfilDtos);
        }

        public async Task<Response<GnPerfilDto>> Add(SaveGnPerfilDto dto)
        {
            var perfil = _mapper.Map<GnPerfil>(dto);
            perfil.FechaCreada = DateTime.Now;

            try
            {
                await _repository.AddAsync(perfil);
                var createdDto = _mapper.Map<GnPerfilDto>(perfil);
                return Response<GnPerfilDto>.Created(createdDto);
            }
            catch (Exception ex)
            {
                return Response<GnPerfilDto>.ServerError("Error al crear el perfil: " + ex.Message);
            }
        }

        public async Task<Response<string>> PatchUpdate(int id, Dictionary<string, object> updatedProperties)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null)
                return Response<string>.NotFound("Perfil no encontrado.");

            try
            {
                await _repository.PatchUpdateAsync(id, updatedProperties);
                return Response<string>.NoContent("Perfil actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return Response<string>.BadRequest(new List<string> { ex.Message });
            }
        }

        public async Task<Response<string>> Delete(int id)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null)
                return Response<string>.NotFound("Perfil no encontrado.");

            await _repository.DeleteAsync(id);
            return Response<string>.NoContent("Perfil eliminado correctamente.");
        }
    }
}
