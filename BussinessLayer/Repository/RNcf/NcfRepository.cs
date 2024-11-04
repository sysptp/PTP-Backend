using Dapper;
using DataLayer.Models.Ncf;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.RNcf
{
    public class NcfRepository(IConfiguration configuration) : INcfRepository
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<bool> CreateNcfAsync(Ncf ncf)
        {
            try
            {
                const string insertQuery = @"INSERT INTO NCF_LOTES_DGII
                (TIPO_COMPROBANTE,SECUENCIA_INICIAL,SECUENCIA_FINAL,DISPONIBLE,SECUENCIA,ID_EMPRESA,FECHA_CREACION,ADICIONADO_POR) 
                VALUES(@TIPO_COMPROBANTE,@SECUENCIA_INICIAL,@SECUENCIA_FINAL,@DISPONIBLE,@SECUENCIA,@ID_EMPRESA,@FECHA_CREACION,@ADICIONADO_POR)";

                var invoice = new
                {
                    TIPO_COMPROBANTE = ncf.NcfType,
                    SECUENCIA_INICIAL = ncf.InitialSequence,
                    SECUENCIA_FINAL = ncf.FinalSequence,
                    DISPONIBLE = ncf.FinalSequence,
                    SECUENCIA = ncf.InitialSequence,
                    ID_EMPRESA = ncf.BussinesId,
                    FECHA_CREACION = DateTime.Now,
                    ADICIONADO_POR = ncf.UserId,
                };
                using SqlConnection connection = new(_configuration.GetConnectionString("POS_CONN"));
                int result = await connection.ExecuteAsync(insertQuery, invoice);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteNcfAsync(string ncfType, int bussinesId)
        {
            try
            {
                const string query = @"UPDATE NCF_LOTES_DGII SET ESTADO = 0 WHERE ID_EMPRESA = @bussinesId and TIPO_COMPROBANTE = @ncfType";

                using SqlConnection connection = new(_configuration.GetConnectionString("POS_CONN"));

                int result = await connection.ExecuteAsync(query, new { bussinesId, ncfType });

                return result == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<Ncf>> GetAllNcfsAsync(int bussinesId)
        {
            try
            {
                const string query = @"SELECT TIPO_COMPROBANTE AS NcfType, SECUENCIA_INICIAL AS InitialSequence, SECUENCIA_FINAL as FinalSequence,
                DISPONIBLE AS AvailableSequence, SECUENCIA AS Sequence FROM NCF_LOTES_DGII WHERE ID_EMPRESA = @bussinesId and ESTADO = 1;";

                using SqlConnection connection = new(_configuration.GetConnectionString("POS_CONN"));

                IEnumerable<Ncf> ncfs = await connection.QueryAsync<Ncf>(query, new { bussinesId });

                return ncfs.ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<Ncf> GetNcfByIdAsync(string ncfType, int bussinesId)
        {
            try
            {
                const string query = @"SELECT TIPO_COMPROBANTE AS NcfType, SECUENCIA_INICIAL AS InitialSequence, SECUENCIA_FINAL as FinalSequence,
                DISPONIBLE AS AvailableSequence, SECUENCIA AS Sequence FROM NCF_LOTES_DGII WHERE ID_EMPRESA = @bussinesId and TIPO_COMPROBANTE = @ncfType and ESTADO = 1;";

                using SqlConnection connection = new(_configuration.GetConnectionString("POS_CONN"));

                Ncf ncf = await connection.QueryFirstAsync<Ncf>(query, new { bussinesId, ncfType });

                return ncf;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
