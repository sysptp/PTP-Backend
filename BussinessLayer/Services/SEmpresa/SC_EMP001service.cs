using DataLayer.PDbContex;
using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SEmpresa
{
    public class SC_EMP001service : ISC_EMP001service
    {
        private readonly PDbContext _context;

        public SC_EMP001service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_EMP001 entity)
        {

            SqlParameter[] idParam = new SqlParameter[17];
            idParam[0] = new SqlParameter() { ParameterName = "FLAG", Value = 2 }; 
            idParam[1] = new SqlParameter() { ParameterName = "CODIGO_EMP", Value = 0}; 
            idParam[2] = new SqlParameter() { ParameterName = "NOMBRE_EMP", Value = entity.NOMBRE_EMP }; 
            idParam[3] = new SqlParameter( ) { ParameterName = "LOGO_EMP", Value = entity.LOGO_EMP }; 
            idParam[4] = new SqlParameter( ) { ParameterName = "RNC_EMP", Value = entity.RNC_EMP };
            idParam[5] = new SqlParameter() { ParameterName = "DIRECCION", Value = entity.DIRECCION };
            idParam[6] = new SqlParameter() { ParameterName = "TELEFONO1", Value = entity.TELEFONO1 };
            idParam[7] = new SqlParameter() { ParameterName = "TELEFONO2", Value = entity.TELEFONO2 };
            idParam[8] = new SqlParameter() { ParameterName = "EXT_TEL1", Value = entity.EXT_TEL1 };
            idParam[9] = new SqlParameter() { ParameterName = "EXT_TEL2", Value = entity.EXT_TEL2 };
            idParam[10] = new SqlParameter() { ParameterName = "CANT_SUCURSALES", Value = entity.CANT_SUCURSALES };
            idParam[11] = new SqlParameter() { ParameterName = "CANT_USUARIO", Value = entity.CANT_USUARIO };
            idParam[12] = new SqlParameter() { ParameterName = "WEB", Value = entity.WEB };
            idParam[13] = new SqlParameter() { ParameterName = "USUARIO_ADICCION", Value = entity.USUARIO_ADICCION };
            idParam[14] = new SqlParameter() { ParameterName = "FECHA_ADICION", Value = entity.FECHA_ADICION };
            idParam[15] = new SqlParameter() { ParameterName = "USUARIO_MODIFICACION", Value = entity.USUARIO_ADICCION };
            idParam[16] = new SqlParameter() { ParameterName = "FECHA_MODIFICACION", Value = entity.FECHA_ADICION };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(@"exec SP_SG_EMP001 @FLAG,
                                                                    @CODIGO_EMP,
                                                                    @NOMBRE_EMP,
                                                                    @LOGO_EMP,
                                                                    @RNC_EMP,
                                                                    @DIRECCION,
                                                                    @TELEFONO1,
                                                                    @TELEFONO2,
                                                                    @EXT_TEL1,
                                                                    @EXT_TEL2,
                                                                    @CANT_SUCURSALES,
                                                                    @CANT_USUARIO,
                                                                    @WEB,
                                                                    @USUARIO_ADICCION,
                                                                    @FECHA_ADICION,
                                                                    @USUARIO_MODIFICACION,
                                                                    @FECHA_MODIFICACION", idParam);
                  
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }

        public async Task<SC_EMP001> GetById(int id)
        {
            return await _context.SC_EMP001.FindAsync(id);
        }
     
        public async Task<IList<SC_EMP001>> GetAll()
        {
            SqlParameter idParam = new SqlParameter() {ParameterName= "FLAG", Value= 1 };
        
            try
            {
                return await _context.Database.SqlQueryRaw<SC_EMP001>(@"exec SP_SG_EMP001
                                                                     @FLAG,
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     '',
                                                                     ''", idParam).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var cl = await _context.Clientes.FindAsync(id);
            if (cl == null) return;

            cl.Borrado = true;
            await _context.SaveChangesAsync();    
        }

        public async Task Edit(SC_EMP001 entity)
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
    }
}
