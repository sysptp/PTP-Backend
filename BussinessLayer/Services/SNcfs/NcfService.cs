using AutoMapper;
using BussinessLayer.DTOs.Ncfs;
using BussinessLayer.Repository.RNcf;
using DataLayer.Models.Ncf;

namespace BussinessLayer.Services.SNcfs
{
    public class NcfService(INcfRepository ncfRepository, IMapper mapper) : INcfService
    {
        private readonly IMapper _mapper = mapper;
        private readonly INcfRepository _ncfRepository = ncfRepository;
        public async Task CreateAsync(CreateNcfDto createNcfDto)
        {
            try
            {
                Ncf ncf = _mapper.Map<Ncf>(createNcfDto);
                await _ncfRepository.CreateNcfAsync(ncf);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(int bussines, string ncfType)
        {
            await _ncfRepository.DeleteNcfAsync(ncfType, bussines);
        }

        public async Task<List<NcfDto>> GetAllAsync(int bussines)
        {
            try
            {
                List<NcfDto> ncfs = _mapper.Map<List<NcfDto>>(await _ncfRepository.GetAllNcfsAsync(bussines));
                return ncfs;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<NcfDto> GetByIdAsync(int bussines, string ncfType)
        {
            try
            {
                NcfDto ncf = _mapper.Map<NcfDto>(await _ncfRepository.GetNcfByIdAsync(ncfType,bussines));
                return ncf;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
