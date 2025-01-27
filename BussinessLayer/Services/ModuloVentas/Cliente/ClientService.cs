using BussinessLayer.DTOs.ModuloVentas.Cliente;
using BussinessLayer.Interfaces.Services.ModuloVentas.IClient;
using AutoMapper;
using BussinessLayer.DTOs.Cliente;
using BussinessLayer.DTOs.Contactos.ClienteContacto;
using BussinessLayer.Interfaces.IClient;
using BussinessLayer.Repository.RClient;
using BussinessLayer.Wrappers;
using DataLayer.Models.Clients;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Cliente
{
    public class ClientService(IClientRepository clientRepository, IMapper mapper, IClientContactRepository clientContactRepository, PDbContext dbContext) : IClientService
    {
        private readonly IMapper _mapper = mapper;
        private readonly PDbContext _dbContext = dbContext;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IClientContactRepository _clientContactRepository = clientContactRepository;
        public async Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto)
        {
            try
            {
                //Mapeando el DTO a la entidad.
                Client client = _mapper.Map<Client>(dto);
                //Enviando a agregar el cliente a la DB.
                client = await _clientRepository.AddAsync(client);

                //Luego de insertar al cliente, se insertan los contactos de ese cliente.
                foreach (ClientContactDto clientContact in dto.ClientContacts)
                {
                    clientContact.ClientId = client.Id;
                    clientContact.BussinesId = client.CodeBussines;
                    await _clientContactRepository.CreateAsync(clientContact);
                }
                //Mapeando al DTO y retornando el dto al endpoint.
                dto = _mapper.Map<CreateClientDto>(client);
                return Response<CreateClientDto>.Created(dto);
            }
            catch (Exception ex)
            {
                return Response<CreateClientDto>.ServerError(ex.Message);
            }
        }
        public async Task<Response<List<Client>>> GetAllAsync(int bussinesId, int pageSize, int pageCount)
        {
            try
            {
                List<Client> clients = await _clientRepository.GetAllAsync(bussinesId, pageSize, pageCount);

                return Response<List<Client>>.Success(clients);
            }
            catch (Exception ex)
            {
                return Response<List<Client>>.ServerError(ex.Message);
            }
        }

        public async Task<Response<Client>> GetByIdAsync(int clientId)
        {
            try
            {
                Client? client = await _dbContext.Clients
               .Include(x => x.ClientContacts)
               .FirstOrDefaultAsync(x => x.Id == clientId);

                return client != null ? Response<Client>.Success(client) : Response<Client>.NoContent();
            }
            catch (Exception ex)
            {
                return Response<Client>.ServerError(ex.Message);
            }

        }
    }
}
