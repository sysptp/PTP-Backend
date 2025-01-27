using AutoMapper;
using BussinessLayer.DTOs.Contactos.ClienteContacto;
using DataLayer.Models.Contactos;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SCliente
{
    public class ClientContactRepository(PDbContext dbContext, IMapper mapper) : IClientContactRepository
    {
        private readonly PDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ClientContact> CreateAsync(ClientContactDto contact)
        {
            try
            {
                ClientContact model = _mapper.Map<ClientContact>(contact);
                await _dbContext.ClientContacts.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return model;

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
