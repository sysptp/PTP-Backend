using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloInventario.Productos
{
    public class TipoProductoService : ITipoProductoService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public TipoProductoService(PDbContext pDbContext, IMapper mapper, ITokenService tokenService)
        {
            _context = pDbContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int?> CreateProductType(CreateTipoProductoDto productType)
        {
            var newType = _mapper.Map<InvTipoProducto>(productType);

            newType.FechaCreacion = DateTime.Now;
            newType.Borrado = false;
            newType.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
            newType.Activo = false;

            _context.InvTipoProductos.Add(newType);
            await _context.SaveChangesAsync();

            return newType.Id;
        }

        public async Task DeleteProductTypeById(int id)
        {
            var type = await _context.InvTipoProductos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (type != null)
            {
                type.Borrado = true;
                type.Activo = false;
                var updated = _mapper.Map<InvTipoProducto>(type);
                _context.Update(updated);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditProductType(EditProductTypeDto type)
        {
            var existingType = await _context.InvTipoProductos
            .FirstOrDefaultAsync(x => x.Id == type.Id);

            var activox = existingType.Activo;

            if (existingType != null)
            {
                _mapper.Map(type, existingType);
                existingType.FechaModificacion = DateTime.Now;
                existingType.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                existingType.Activo = activox;
                _context.InvTipoProductos.Update(existingType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ViewProductTypeDto>> GetAllProductsTypeByComp(int idCompany)
        {
            var data = await _context.InvTipoProductos
                .Where(x => x.Borrado == false && x.IdEmpresa == idCompany).ToListAsync();

            return _mapper.Map<List<ViewProductTypeDto>>(data);
        }

        public async Task<ViewProductTypeDto> GetProductTypeById(int id)
        {
            var data = await _context.InvTipoProductos
            .Where(x => x.Id == id)
            .Where(x => x.Borrado == false)
            .FirstOrDefaultAsync();

            return _mapper.Map<ViewProductTypeDto>(data);
        }
    }
}
