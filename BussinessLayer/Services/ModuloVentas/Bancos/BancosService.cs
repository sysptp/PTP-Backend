using BussinessLayer.Interfaces.ModuloVentas.IBancos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

    public class BancosService : IBancosService
    {
        private readonly PDbContext _context;

        public BancosService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Bancos model)
        {
            _context.Bancos.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Bancos model)
        {
            _context.Bancos.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bancos>> GetAll()
        {
            return await _context.Bancos.ToListAsync();
        }

        public async Task<List<Bancos>> GetAllIndex()
        {
            var data = _context.Bancos
                .Include(b => b.empresa)
                .Include(b => b.sucursal);

            return await data.ToListAsync();
        }

        public async Task<Bancos> GetById(int id)
        {
            var result = await _context.Bancos.FindAsync(id);

            return result;
        }

        public async Task Update(Bancos model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

