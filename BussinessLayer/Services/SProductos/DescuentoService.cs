using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Interface.IProductos;
using DataLayer.Models.Productos;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SProductos
{
    public class DescuentoService : IDescuentoService
    {
        private readonly PDbContext _context;

        public DescuentoService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Descuentos entity)
        {
            try
            {
                entity.Activo = DateTime.Today >= entity.FechaInicio && DateTime.Today <= entity.FechaFin;
                _context.Descuentos.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }     
        }

        public async Task Edit(Descuentos entity)
        {
            var descuentoToEdit = await _context.Descuentos.FindAsync(entity.Id);
            if (descuentoToEdit == null) return;

            descuentoToEdit.IdProducto = entity.IdProducto;
            descuentoToEdit.Activo = entity.Activo;
            descuentoToEdit.DescuentoFijo = entity.DescuentoFijo;
            descuentoToEdit.DescuentoPorcentaje = entity.DescuentoPorcentaje;
            descuentoToEdit.FechaFin = entity.FechaFin;
            descuentoToEdit.FechaInicio = entity.FechaInicio;
            descuentoToEdit.TipoDescuento = entity.TipoDescuento;
            descuentoToEdit.Borrado = entity.Borrado;
                
            await _context.SaveChangesAsync();         
        }

        public async Task<Descuentos> GetById(int id, long idEMpresa)
        {
            return await _context.Descuentos.Include(x => x.Producto).SingleOrDefaultAsync(x => x.Id.Equals(id));            
        }

        public async Task<IList<Descuentos>> GetAll(long idEMpresa)
        {
            var list = await _context.Descuentos.Include(x => x.Producto.Version.Marca).Where(x => x.Borrado == false).ToListAsync();

            if (!list.Any()) return list;
            {
                foreach (var x in list)
                {
                    x.NombreProducto = $"{x.Producto?.Version.Marca.Nombre} {x.Producto?.Version.Nombre}";
                }
            }

            return list;
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var descuento = await GetById(id,idEMpresa);
            if (descuento == null) return;

            descuento.Borrado = true;
            await Edit(descuento);
        }
    }
}