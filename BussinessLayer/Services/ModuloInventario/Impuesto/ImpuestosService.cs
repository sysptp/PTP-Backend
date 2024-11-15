using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloInventario.Impuesto
{
    public class ImpuestosService : IImpuestosService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ImpuestosService(PDbContext dbContext,
            IMapper mapper,
            ITokenService tokenService)
        {
            _context = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // Obtener data por su id
        public async Task<ViewTaxDto> GetTaxById(int id)
        {
            var data = await _context.Impuestos
                .Include(x => x.Moneda)
                .Where(x => x.Id == id && x.Borrado == false)
                .FirstOrDefaultAsync();
            return _mapper.Map<ViewTaxDto>(data);
        }

        // Obtener data por su empresa
        public async Task<List<ViewTaxDto>> GetTaxByCompany(int id)
        {
            var data = await _context.Impuestos
                .Include(x => x.Moneda)
                .Where(x => x.IdEmpresa == id && x.Borrado == false)
                .ToListAsync();

            return _mapper.Map<List<ViewTaxDto>>(data);
        }

        // Crear un nuevo 
        public async Task<int?> CreateTax(CreateTaxDto create)
        {
            var newObject = _mapper.Map<InvImpuestos>(create);
            newObject.FechaCreacion = DateTime.Now;
            newObject.Borrado = false;
            newObject.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

            _context.Impuestos.Add(newObject);
            await _context.SaveChangesAsync();

            return newObject.Id;
        }

        // Editar existente
        public async Task EditTax(EditTaxDto edit)
        {
            var existing = await _context.Impuestos.FirstOrDefaultAsync(x => x.Id == edit.Id);

            if (existing != null)
            {
                _mapper.Map(edit, existing);
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                existing.FechaModificacion = DateTime.Now;
                _context.Impuestos.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        // Servicio para eliminar por id unico
        public async Task DeleteTaxById(int id)
        {
            var data = await _context.Impuestos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data != null)
            {
                data.Borrado = true;
                _context.Impuestos.Update(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
