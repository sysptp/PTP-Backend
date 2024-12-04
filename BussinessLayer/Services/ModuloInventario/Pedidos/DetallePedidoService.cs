using AutoMapper;
using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.Interfaces.ModuloInventario.Pedidos;
using DataLayer.Models.ModuloInventario.Pedidos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloInventario.Pedidos
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly PDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public DetallePedidoService(PDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<int> Add(CreateDetallePedidoDto model)
        {
            var newData = _mapper.Map<DetallePedido>(model);
            newData.FechaCreacion = DateTime.Now;
            newData.Borrado = false;
            newData.UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

            _context.DetallePedido.Add(newData);
            await _context.SaveChangesAsync();
            return newData.Id;
        }

        public async Task Update(EditDetallePedidoDto model)
        {
            var existing = await _context.DetallePedido.FirstOrDefaultAsync(x => x.Id == model.Id && !x.Borrado);
            if (existing != null)
            {
                _mapper.Map(model, existing);
                existing.FechaModificacion = DateTime.Now;
                existing.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
                _context.DetallePedido.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var existing = await _context.DetallePedido.FirstOrDefaultAsync(x => x.Id == id && !x.Borrado);
            if (existing != null)
            {
                existing.Borrado = true;
                _context.DetallePedido.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ViewDetallePedidoDto> GetById(int id)
        {
            var entity = await _context.DetallePedido.FirstOrDefaultAsync(x => x.Id == id && !x.Borrado);
            return _mapper.Map<ViewDetallePedidoDto>(entity);
        }

        public async Task<List<ViewDetallePedidoDto>> GetAll()
        {
            var entities = await _context.DetallePedido.Where(x => !x.Borrado).ToListAsync();
            return _mapper.Map<List<ViewDetallePedidoDto>>(entities);
        }

        public async Task<List<ViewDetallePedidoDto>> GetByCompany(long idEmpresa)
        {
            var entities = await _context.DetallePedido.Where(x => x.IdEmpresa == idEmpresa && !x.Borrado).ToListAsync();
            return _mapper.Map<List<ViewDetallePedidoDto>>(entities);
        }
    }
}
