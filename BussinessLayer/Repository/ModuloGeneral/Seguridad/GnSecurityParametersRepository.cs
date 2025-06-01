using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using Dapper;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.ModuloGeneral.Seguridad
{
    public class GnSecurityParametersRepository : GenericRepository<GnSecurityParameters>, IGnSecurityParametersRepository
    {
        private readonly ITokenService _tokenService;
        protected readonly PDbContext _context;
        private readonly string _connectionString;

        public GnSecurityParametersRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _context = dbContext;
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

        public override async Task<GnSecurityParameters> Add(GnSecurityParameters entity)
        {
            try
            {
                entity.FechaAdicion = DateTime.Now;
                entity.UsuarioAdicion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";

                const string sql = @"
            INSERT INTO GnSecurityParameters (
                CompanyId,
                Requires2FA,
                AllowsOptional2FA,
                PasswordExpirationDays,
                Borrado,
                FechaAdicion,
                UsuarioAdicion,
                FechaModificacion,
                UsuarioModificacion
            )
            VALUES (
                @CompanyId,
                @Requires2FA,
                @AllowsOptional2FA,
                @PasswordExpirationDays,
                @Borrado,
                @FechaAdicion,
                @UsuarioAdicion,
                @FechaModificacion,
                @UsuarioModificacion
            )";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(sql, entity);
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error insertando GnSecurityParameters", ex);
            }
        }

    }
}
