using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class MunicipioService : IMunicipioService
    {
        private readonly PDbContext _context;

        public MunicipioService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        //public Task Add(Municipio entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Municipio> GetById(int id, long idEMpresa)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<Municipio>> GetAll(long idEMpresa)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task Delete(int id, long idEMpresa)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId)
        {

            return _context.Municipios.ToList().Where(x => x.Borrado != true && x.IdProvincia == provinciaId);
        }

        public async Task Add(Municipio model)
        {
            _context.Municipios.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Municipio model)
        {
            _context.Municipios.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Municipio>> GetAll()
        {
            return await _context.Municipios.ToListAsync();
        }

        public async Task<Municipio> GetById(int id)
        {
            var result = await _context.Municipios.FindAsync(id);

            return result;
        }

        public async Task Update(Municipio model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Municipio>> GetIndex()
        {
            var municipios = await _context.Municipios.Include(m => m.Provincia).ToListAsync();

            return municipios;
        }
        //public Task Edit(Municipio entity)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IBaseService<Municipio>.Edit(Municipio entity)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Municipio> IBaseService<Municipio>.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<Municipio>> IBaseService<Municipio>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //Task IBaseService<Municipio>.Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
