using AutoMapper;
using BussinessLayer.DTOs.Ncfs;
using BussinessLayer.Repository.RNcf;
using BussinessLayer.Wrappers;
using DataLayer.Models.Ncf;

namespace BussinessLayer.Services.SNcfs
{
    public class NcfService(INcfRepository ncfRepository, IMapper mapper) : INcfService
    {
        private readonly IMapper _mapper = mapper;
        private readonly INcfRepository _ncfRepository = ncfRepository;
        public async Task<Response<CreateNcfDto>> CreateAsync(CreateNcfDto createNcfDto)
        {
            try
            {
                Ncf isCreated = await _ncfRepository.GetNcfByIdAsync(createNcfDto.NcfType, createNcfDto.BussinesId);

                if (isCreated != null) return Response<CreateNcfDto>.BadRequest([$"La empresa aun tiene secuencias activas del tipo {createNcfDto.NcfType}"]);

                Ncf ncf = _mapper.Map<Ncf>(createNcfDto);

                bool created = await _ncfRepository.CreateNcfAsync(ncf);

                return created ? Response<CreateNcfDto>.Created(createNcfDto) : Response<CreateNcfDto>.BadRequest(["Ocurrio un error al crear la secuencia"]);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        public async Task<Response<string>> DeleteAsync(int bussines, string ncfType)
        {
            if (string.IsNullOrEmpty(ncfType)) return Response<string>.BadRequest(["El tipo de comprobante no puede ser nulo"]);

            Ncf isCreated = await _ncfRepository.GetNcfByIdAsync(ncfType, bussines);

            if (isCreated == null) return Response<string>.BadRequest([$"La empresa no tiene secuencias activas del tipo {ncfType}"]);

            bool result = await _ncfRepository.DeleteNcfAsync(ncfType, bussines);

            return result ? Response<string>.NoContent() : Response<string>.BadRequest([$"Ocurrio un error al tratar de eliminar la secuencia"]);
        }

        public async Task<Response<List<NcfDto>>> GetAllAsync(int bussines)
        {
            try
            {
                List<NcfDto> ncfs = _mapper.Map<List<NcfDto>>(await _ncfRepository.GetAllNcfsAsync(bussines));

                return ncfs.Count > 0 ? Response<List<NcfDto>>.Success(ncfs) : Response<List<NcfDto>>.NoContent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<Response<NcfDto>> GetByIdAsync(int bussines, string ncfType)
        {
            try
            {
                if (string.IsNullOrEmpty(ncfType)) return Response<NcfDto>.BadRequest(["El tipo de comprobante no puede ser nulo"]);

                NcfDto ncf = _mapper.Map<NcfDto>(await _ncfRepository.GetNcfByIdAsync(ncfType, bussines));

                return ncf != null ? Response<NcfDto>.Success(ncf) : Response<NcfDto>.NoContent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
