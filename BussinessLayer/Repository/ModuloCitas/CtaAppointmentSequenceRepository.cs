using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using Dapper;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaAppointmentSequenceRepository : GenericRepository<CtaAppointmentSequence>, ICtaAppointmentSequenceRepository
    {
        private readonly string _connectionString;
        public CtaAppointmentSequenceRepository(PDbContext dbContext, ITokenService tokenService,
            IConfiguration configuration) : base(dbContext, tokenService)
        {
            _connectionString = configuration.GetConnectionString("POS_CONN");
        }

        public override async Task<IList<CtaAppointmentSequence>> GetAll()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<CtaAppointmentSequence>("SELECT * FROM CtaAppointmentSequence WHERE Borrado = 0");
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos: {ex.Message}", ex);
            }
        }

    }
}
