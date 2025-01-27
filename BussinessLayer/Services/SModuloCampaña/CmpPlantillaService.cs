using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas;
using BussinessLayer.FluentValidations.Generic;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Services.SModuloCampaña
{
    public class CmpPlantillaService(ICmpPlantillaRepository repository,
        IMapper mapper,
        IValidateService<CmpPlantillaCreateDto> postValidate,
        IValidateService<CmpPlantillaUpdateDto> putValidate) : ICmpPlantillaService
    {
        public async Task<Response<CmpPlantillaCreateDto>> CreateAsync(CmpPlantillaCreateDto createDto)
        {
            try
            {
                List<string> errors = postValidate.Validate(createDto);
                if (errors != null) return Response<CmpPlantillaCreateDto>.BadRequest(errors);

                CmpPlantillas plantilla = mapper.Map<CmpPlantillas>(createDto);
                await repository.AddAsync(plantilla);
                return plantilla.Id != 0 ? Response<CmpPlantillaCreateDto>.Created(createDto) : Response<CmpPlantillaCreateDto>.ServerError("Ocurrio un error, favor revisar los logs");
            }
            catch (Exception ex)
            {
                return Response<CmpPlantillaCreateDto>.ServerError(ex.Message);
            }
        }

        public async Task<Response<List<CmpPlantillaDto>>> GetAllAsync(int empresaId)
        {
            try
            {
                List<CmpPlantillas> plantillas = await repository.GetAllAsync(empresaId);
                if (plantillas == null || plantillas.Count <= 0) return Response<List<CmpPlantillaDto>>.NotFound();

                List<CmpPlantillaDto> plantillaDtos = mapper.Map<List<CmpPlantillaDto>>(plantillas);
                return Response<List<CmpPlantillaDto>>.Success(plantillaDtos);
            }
            catch (Exception ex)
            {
                return Response<List<CmpPlantillaDto>>.ServerError(ex.Message);
            }
        }

        public async Task<Response<CmpPlantillaDto>> GetByIdAsync(int id, int empresaId)
        {
            try
            {
                CmpPlantillas plantilla = await repository.GetByIdAsync(id, empresaId);
                if (plantilla == null) return Response<CmpPlantillaDto>.NotFound();

                CmpPlantillaDto plantillaDtos = mapper.Map<CmpPlantillaDto>(plantilla);
                return Response<CmpPlantillaDto>.Success(plantillaDtos);
            }
            catch (Exception ex)
            {
                return Response<CmpPlantillaDto>.ServerError(ex.Message);
            }
        }

        public async Task<Response<CmpPlantillaUpdateDto>> UpdateAsync(CmpPlantillaUpdateDto updateDto)
        {
            try
            {
                List<string> errors = putValidate.Validate(updateDto);
                if (errors != null) return Response<CmpPlantillaUpdateDto>.BadRequest(errors);

                CmpPlantillas plantilla = mapper.Map<CmpPlantillas>(updateDto);

                await repository.UpdateAsync(plantilla);

                return Response<CmpPlantillaUpdateDto>.Success(updateDto);
            }
            catch (Exception ex)
            {
                return Response<CmpPlantillaUpdateDto>.ServerError(ex.Message);
            }
        }
    }
}
