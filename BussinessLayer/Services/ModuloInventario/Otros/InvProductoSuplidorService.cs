using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloInventario.Otros;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloInventario.Otros
{
    public class InvProductoSuplidorService : IInvProductoSuplidorService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public InvProductoSuplidorService(PDbContext dbContext,
            IMapper mapper, ITokenService tokenService)
        {
            _context = dbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateInvProductoSuplidorDTO model)
        {
            var newDta = _mapper.Map<InvProductoSuplidor>(model);

            newDta.FechaCreacion = DateTime.Now;
            newDta.Borrado = false;
            newDta.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            newDta.Activo = false;

            _context.InvProductoSuplidors.Add(newDta);
            await _context.SaveChangesAsync();

            return newDta.Id;
        }

        public async Task Delete(int Id)
        {
            var data = await _context.InvProductoSuplidors
                .FirstOrDefaultAsync(x => x.Id == Id
                && x.Borrado == false);

            if (data != null)
            {
                data.Borrado = true;
                data.Activo = false;
                _context.Update(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ViewInvProductoSuplidorDTO>> GetAll()
        {
            var list = await _context.InvProductoSuplidors
                .Where(x => x.Borrado == false)
                .ToListAsync();

            return _mapper.Map<List<ViewInvProductoSuplidorDTO>>(list);
        }

        public async Task<List<ViewInvProductoSuplidorDTO>> GetAllBySupplier(int idSuplidor)
        {
            var list = await _context.InvProductoSuplidors
                .Where(x => x.Borrado == false && x.SuplidorId == idSuplidor)
                .ToListAsync();

            return _mapper.Map<List<ViewInvProductoSuplidorDTO>>(list);
        }

        public async Task<List<ViewInvProductoSuplidorDTO>> GetAllByProduct(int idProducto)
        {
            var list = await _context.InvProductoSuplidors
                .Where(x => x.Borrado == false && x.ProductoId == idProducto)
                .ToListAsync();

            return _mapper.Map<List<ViewInvProductoSuplidorDTO>>(list);
        }

        public async Task<ViewInvProductoSuplidorDTO> GetById(int id)
        {
            var data = await _context.InvProductoSuplidors
                .FirstOrDefaultAsync(x => x.Id == id
                && x.Borrado == false);

            return _mapper.Map<ViewInvProductoSuplidorDTO>(data);
        }

        public async Task Update(EditInvProductoSuplidorDTO model)
        {
            var existing = await _context.InvProductoSuplidors
                .FirstOrDefaultAsync(x => x.Id == model.Id
                && x.Borrado == false);

            var activox = existing.Activo;

            if (existing != null)
            {
                //_mapper.Map(producto, existing);
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                existing.Activo = activox;
                _context.InvProductoSuplidors.Update(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
