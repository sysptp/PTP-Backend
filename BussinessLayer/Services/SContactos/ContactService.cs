using AutoMapper;
using BussinessLayer.DTOs.Contactos.TypeContact;
using BussinessLayer.Wrappers;
using DataLayer.Models.Contactos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SContactos
{
    public class ContactService(PDbContext dbContext, IMapper mapper) : IContactService
    {
        public async Task<Response<TypeContactRequest>> CreateContactAsync(TypeContactRequest typeContactRequest)
        {
            try
            {

                TypeContact typeContact = mapper.Map<TypeContact>(typeContactRequest);
                await dbContext.TypeContacts.AddAsync(typeContact);
                await dbContext.SaveChangesAsync();
                return Response<TypeContactRequest>.Success(typeContactRequest);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            finally { dbContext.Dispose(); }

        }

        public async Task<Response<List<TypeContactDto>>> GetTypeContactAsync(int bussinesId)
        {
            try
            {
                List<TypeContact> typeContacts = await dbContext.TypeContacts
                .Where(x => x.BussinesId == bussinesId)
                .ToListAsync();

                if (typeContacts == null || typeContacts.Count == 0) return Response<List<TypeContactDto>>.Success(new List<TypeContactDto>(), "OPERACION EXISTOSA, PERO NO HAY REGISTROS");

                List<TypeContactDto> typeContactDtos = mapper.Map<List<TypeContactDto>>(typeContacts);

                return Response<List<TypeContactDto>>.Success(typeContactDtos);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
