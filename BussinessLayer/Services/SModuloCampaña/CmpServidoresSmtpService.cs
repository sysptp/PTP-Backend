using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;
using BussinessLayer.FluentValidations;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Services.SModuloCampaña
{
    public class CmpServidoresSmtpService(ICmpServidoresSmtpRepository repository, IMapper mapper, IValidateService<CmpServidoresSmtpCreateDto> postValidate, IValidateService<CmpServidoresSmtpUpdateDto> putValidate) : ICmpServidoresSmtpService
    {
        public async Task<Response<CmpServidoresSmtpCreateDto>> CreateAsync(CmpServidoresSmtpCreateDto cmpServidoresSmtpCreateDto)
        {
            try
            {
                List<string> errors = postValidate.Validate(cmpServidoresSmtpCreateDto);
                if (errors != null) return Response<CmpServidoresSmtpCreateDto>.BadRequest(errors);

                CmpServidoresSmtp cmpServidoresSmtp = mapper.Map<CmpServidoresSmtp>(cmpServidoresSmtpCreateDto);
                await repository.AddAsync(cmpServidoresSmtp);

                return Response<CmpServidoresSmtpCreateDto>.Success(cmpServidoresSmtpCreateDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Task<Response<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<CmpServidoresSmtpDto>>> GetAllAsync()
        {
            try
            {
                List<CmpServidoresSmtp> entities = await repository.GetAllAsync();

                if (entities.Count < 0 || entities == null) return Response<List<CmpServidoresSmtpDto>>.NoContent();

                List<CmpServidoresSmtpDto> cmpSrvidoresSmtps = mapper.Map<List<CmpServidoresSmtpDto>>(entities);

                return Response<List<CmpServidoresSmtpDto>>.Success(cmpSrvidoresSmtps);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Response<CmpServidoresSmtpDto>> GetByIdAsync(int id)
        {
            try
            {
                CmpServidoresSmtp? entity = await repository.GetByIdAsync(id);
                CmpServidoresSmtpDto? smtpDto = mapper.Map<CmpServidoresSmtpDto?>(entity);
                return Response<CmpServidoresSmtpDto>.Success(smtpDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Response<CmpServidoresSmtpUpdateDto>> UpdateAsync(CmpServidoresSmtpUpdateDto cmpServidoresSmtpUpdateDto)
        {
            try
            {
                CmpServidoresSmtp existing = await repository.GetByIdAsync(cmpServidoresSmtpUpdateDto.ServidorId);
                if (existing == null) return Response<CmpServidoresSmtpUpdateDto>.BadRequest(new List<string> { "El servidor que intenta modificar no existe" });

                List<string> errors = putValidate.Validate(cmpServidoresSmtpUpdateDto);

                if (errors != null) return Response<CmpServidoresSmtpUpdateDto>.BadRequest(errors);

                CmpServidoresSmtp cmpServidoresSmtp = mapper.Map<CmpServidoresSmtp>(cmpServidoresSmtpUpdateDto);
                await repository.UpdateAsync(cmpServidoresSmtp);

                return Response<CmpServidoresSmtpUpdateDto>.Success(cmpServidoresSmtpUpdateDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
