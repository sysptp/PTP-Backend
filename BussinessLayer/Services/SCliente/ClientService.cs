using AutoMapper;
using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Interfaces.IClient;
using BussinessLayer.Repository.RClient;
using BussinessLayer.Wrappers;
using DataLayer.Models.Clients;

namespace BussinessLayer.Services.SCliente
{
    public class ClientService(IClientRepository clientRepository, IMapper mapper) : IClientService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IClientRepository _clientRepository = clientRepository;
        public async Task<Response<CreateClientDto>> CreateAsync(CreateClientDto dto)
        {
            try
            {
                Client client = _mapper.Map<Client>(dto);
                client = await _clientRepository.AddAsync(client);
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
                List<Client> clients = await _clientRepository.GetAllAsync(bussinesId, pageSize,pageCount);

                return Response<List<Client>>.Success(clients);
            }
            catch (Exception ex)
            {
                return Response<List<Client>>.ServerError(ex.Message);
            }
        }
    }
}
