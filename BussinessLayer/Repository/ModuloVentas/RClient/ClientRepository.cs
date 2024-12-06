using BussinessLayer.DTOs.ModuloVentas.Cliente;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.ModuloVentas.RClient
{
    public class ClientRepository(IConfiguration configuration) : IClientRepository
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task CreateAsync(CreateClientDto client)
        {
            try
            {
                var model = new
                {
                    IdEmpresa = client.CodeBussines,
                    TipoIdentificacion = client.CodeTypeIdentification,
                    NumeroIdentificacion = client.Identification,
                    Nombres = client.Name,
                    Apellidos = client.LastName,
                    TelefonoPrincipal = client.Phone,
                    DireccionPrincipal = client.Address,
                    client.Email,
                    PaginaWeb = client.WebSite,
                    Descripcion = client.Description,
                    UsuarioAdicion = client.AddedBy,
                    FechaAdicion = DateTime.Now,
                    Borrado = 0
                };

                const string query = @"INSERT INTO Clientes(IdEmpresa,TipoIdentificacion,NumeroIdentificacion,
	            Nombres,Apellidos,TelefonoPrincipal,DireccionPrincipal,Email,PaginaWeb,Descripcion,UsuarioAdicion,FechaAdicion,Borrado)
	            values(@IdEmpresa,@TipoIdentificacion,@NumeroIdentificacion,
	            @Nombres,@Apellidos,@TelefonoPrincipal,@DireccionPrincipal,@Email,@PaginaWeb,@Descripcion,@UsuarioAdicion,@FechaAdicion,@Borrado)";

                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("POS_CONN"));
                await sqlConnection.ExecuteAsync(query, model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
