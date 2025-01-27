using BussinessLayer.DTOs.ModuloVentas.Cliente;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DataLayer.Models.Clients;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloVentas.RClient
{
    public class ClientRepository(PDbContext context) : IClientRepository
    {
        private readonly PDbContext _context = context;
        public async Task<Client> AddAsync(Client client)
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
                client.DateAdded = DateTime.Now;
                client.DateModified = new DateTime(1793, 1, 1);
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                return client;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<List<Client>> GetAllAsync(int bussinesId,int pageSize,int pageCount)
        {
            try
            {
                return await _context.Clients
                    .Where(x=> x.IsDeleted != 1 && string.Equals(x.CodeBussines,bussinesId))
                    .OrderBy(c=> c.Id)
                    .Skip((pageSize - 1) * pageCount)
                    .Take(pageCount)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
