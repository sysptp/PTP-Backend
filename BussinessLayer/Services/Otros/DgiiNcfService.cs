using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SOtros
{
    public class DgiiNcfService : IDgiiNcfService
    {
        private readonly PDbContext _context;

        public DgiiNcfService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddDgii(DgiiNcf entity)
        {
            try
            {
                _context.DgiiNcf.Add(entity);
                await _context.SaveChangesAsync();

                var id = await _context.DgiiNcf.MaxAsync(x => x.Id);

                for (int n = 1; n <= 20; n++)
                {
                    _context.DgiiNcfSecuencia.Add(new DgiiNcfSecuencia()
                    {
                        IdDgiiNcf = id,
                        Secuecial = int.Parse(entity.SecuencialInicial) + n,
                        SerieTipoComprobante = entity.Serie + entity.TipoComprobante,
                        FechaModificacion = DateTime.Now,
                        Estado = false,
                        Borrado = false,
                        FechaCreacion = DateTime.Now
                    });
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task EditDgii(DgiiNcf entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<DgiiNcf> GetByIdDgii(int id)
        {
            return await _context.DgiiNcf.FindAsync(id);
        }

        public async Task<IList<DgiiNcf>> GetAllDgii()
        {
            return await _context.DgiiNcf.Where(x => x.Borrado != true).ToListAsync();
        }

        public async Task Delete(int id, long idEMpresa)
        {
            var d = await _context.DgiiNcf.FindAsync(id);
            if (d != null)
            {
                d.Borrado = true;
                await _context.SaveChangesAsync();
            }
        }

        public Task Add(DgiiNcfSecuencia entity)
        {
            throw new NotImplementedException();
        }

        public async Task Edit(DgiiNcfSecuencia entity)
        {
            try
            {
                //Modigico el Status a Enviado
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                //Tomo la secuencia mas alta
                var dgiSec = await _context.DgiiNcfSecuencia.Include(x => x.DgiiNcf)
                    .Where(x => x.Estado != true && x.IdDgiiNcf == entity.IdDgiiNcf).ToListAsync();
                var seq2 = dgiSec.OrderByDescending(x => x.Secuecial).FirstOrDefault();


                if (seq2.Secuecial <= int.Parse(seq2.DgiiNcf.SecuenciaDgii))
                {

                   //Hago conteo de los activos
                    int activeCount = await _context.DgiiNcfSecuencia.CountAsync(x => x.IdDgiiNcf == entity.IdDgiiNcf && x.Estado != true);


                    //Si es menor a 20, creo nuevamente NCF hasta llegar a 20
                    if (activeCount < 20)
                    {
                        int howManyElse = 20 - activeCount;

                        for (int x = 1; x <= howManyElse; x++)
                        {
                            int seqSum = seq2.Secuecial + x;
                            _context.DgiiNcfSecuencia.Add(new DgiiNcfSecuencia()
                            {
                                IdDgiiNcf = seq2.IdDgiiNcf,
                                Secuecial = seqSum,
                                SerieTipoComprobante = entity.SerieTipoComprobante,
                                FechaModificacion = DateTime.Now,
                                Estado = false,
                                Borrado = false,
                                FechaCreacion = DateTime.Now
                            });
                          
                            await _context.SaveChangesAsync();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        Task<DgiiNcfSecuencia> IBaseService<DgiiNcfSecuencia>.GetById(int id, long idEMpresa)
        {
            throw new NotImplementedException();
        }

        Task<IList<DgiiNcfSecuencia>> IBaseService<DgiiNcfSecuencia>.GetAll(long idEMpresa)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DgiiNcfSecuencia>> GetLastSecuence()
        {
            return await _context.DgiiNcfSecuencia.Where(x => x.Estado != true).ToListAsync();

        }

        public async Task<string> GetSeqNcfByTipoNcf(int tipoNcf)
        {
            var lst = await _context.DgiiNcfSecuencia.Where(x => x.Estado != true && x.IdDgiiNcf == tipoNcf).ToListAsync();

            int seq = lst.Min(x => x.Secuecial);

            var dgi = await _context.DgiiNcfSecuencia.SingleOrDefaultAsync(x => x.Secuecial == seq && x.IdDgiiNcf == tipoNcf);
            string ncfTotal = dgi.SerieTipoComprobante + dgi.Secuecial.ToString().PadLeft(8, '0');
            return ncfTotal;

        }

        public async Task<DgiiNcfSecuencia> FindNcfBySequencial(string ncfSeq)
        {
            string ncf = ncfSeq.Substring(3);
            int seq = int.Parse(ncf);
            return await _context.DgiiNcfSecuencia.SingleOrDefaultAsync(x => x.Secuecial == seq);
        }

        //public async Task<IEnumerable<TipoNcf>> GetTiposNcf()
        //{
        //    return await _context.TipoNcf.Where(x => x.Borrado != true).ToListAsync();
        //}

    }
}
