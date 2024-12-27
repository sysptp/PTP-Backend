using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Monedas;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.Services.ModuloInventario.Suplidores;

namespace BussinessLayer.Services.ModuloInventario.Suplidores
{
    public class ContactosSuplidoresService : IContactosSuplidoresService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ContactosSuplidoresService(PDbContext dbContext,
            IMapper mapper, ITokenService tokenService)
        {
            _context = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateContactosSuplidoresDto model)
        {
            var newDta = _mapper.Map<ContactosSuplidores>(model);

            newDta.FechaCreacion = DateTime.Now;
            newDta.Borrado = false;
            newDta.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

            _context.ContactosSuplidores.Add(newDta);
            await _context.SaveChangesAsync();

            return newDta.Id;
        }

        public async Task Delete(int Id)
        {
            var data = await _context.ContactosSuplidores
                .FirstOrDefaultAsync(x => x.Id == Id
                && x.Borrado == false);

            if (data != null)
            {
                data.Borrado = true;
                _context.Update(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ViewContactosSuplidoresDto>> GetAll()
        {
            var list = await _context.ContactosSuplidores
                .Where(x => x.Borrado == false)
                .ToListAsync();

            return _mapper.Map<List<ViewContactosSuplidoresDto>>(list);
        }

        public async Task<ViewContactosSuplidoresDto> GetById(int id)
        {
            var product = await _context.ContactosSuplidores
                .FirstOrDefaultAsync(x => x.Id == id
                && x.Borrado == false);

            return _mapper.Map<ViewContactosSuplidoresDto>(product);
        }

        public async Task<List<ViewContactosSuplidoresDto>> GetByCompany(int idEmpresa)
        {
            var product = await _context.ContactosSuplidores
                .FirstOrDefaultAsync(x => x.Borrado == false
                && x.IdEmpresa == idEmpresa);

            return _mapper.Map<List<ViewContactosSuplidoresDto>>(product);
        }

        public async Task Update(EditContactosSuplidoresDto model)
        {
            var existing = await _context.ContactosSuplidores
                .FirstOrDefaultAsync(x => x.Id == model.Id
                && x.Borrado == false);


            if (existing != null)
            {
                //_mapper.Map(producto, existing);
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                _context.ContactosSuplidores.Update(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
