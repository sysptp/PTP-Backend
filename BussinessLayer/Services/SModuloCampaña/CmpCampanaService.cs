using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpCampana;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Services.SModuloCampaña
{
    public class CmpCampanaService(ICmpCampanaRepository repository, IMapper mapper) : ICmpCampanaService
    {
        public async Task<Response<CmpCampanaCreateDto>> CreateAsync(CmpCampanaCreateDto dto)
        {
            try
            {
                //Validar el modelo.

                CmpCampana cmpCampana = mapper.Map<CmpCampana>(dto);
                cmpCampana.EstadoId = 1;
                await repository.CreateAsync(cmpCampana);

                return cmpCampana.CampanaId != 0 ? Response<CmpCampanaCreateDto>.Created(dto)
                    : Response<CmpCampanaCreateDto>.ServerError("Error al crear la campaña, favor revisar los logs");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
