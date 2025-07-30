using System.Data;
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
    public class CtaAppointmentGuestRepository : GenericRepository<CtaAppointmentGuest>, ICtaAppointmentGuestRepository
    {
        private readonly ITokenService _tokenService;
        private readonly string _connectionString;


        public CtaAppointmentGuestRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _tokenService = tokenService;
            _connectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("POS_CONN");
        }

        public async Task<List<CtaGuest>> GetAllGuestByAppointmentId(int appointmentId)
        {
            return await _context.Set<CtaAppointmentGuest>()
                .Where(ac => ac.AppointmentId == appointmentId && ac.Borrado == false)
                .Select(ac => ac.Guest)
                .Where(c => c != null && c.Borrado == false)
                .ToListAsync();
        }

        public async Task DeleteByAppointmentId(int appointmentId, int guestId)
        {
            try
            {
                var dbContext = _context;
                var entityType = dbContext.Model.FindEntityType(typeof(CtaAppointmentGuest));
                var tableName = entityType?.GetTableName();

                var fechaModificacion = DateTime.Now;
                var usuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                var sql = $"UPDATE {tableName} SET Borrado = @Borrado, FechaModificacion = @FechaModificacion, UsuarioModificacion = @UsuarioModificacion WHERE AppointmentId = @AppointmentId AND GuestId = @GuestId";

                var parameters = new DynamicParameters();
                parameters.Add("@AppointmentId", appointmentId);
                parameters.Add("@GuestId", guestId);
                parameters.Add("@Borrado", true);
                parameters.Add("@FechaModificacion", fechaModificacion);
                parameters.Add("@UsuarioModificacion", usuarioModificacion);

                using (var connection = new SqlConnection(_connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();

                    var rowsAffected = await connection.ExecuteAsync(sql, parameters);

                    if (rowsAffected == 0)
                        throw new InvalidOperationException("No se encontró el invitado asociado a la cita para eliminar.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar el invitado de la cita: {ex.Message}", ex);
            }
        }

    }
}
