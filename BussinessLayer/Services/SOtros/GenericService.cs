using BussinessLayer.Interfaces.Repositories;
using AutoMapper;
using BussinessLayer.Interfaces.IOtros;

namespace BussinessLayer.Services
{
    public class GenericService<SaveDto, Dto, Model> : IGenericService<SaveDto, Dto,Model> where SaveDto : class
            where Dto : class
            where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(SaveDto vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.Update(entity);
        }

        public virtual async Task<SaveDto> Add(SaveDto vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.Add(entity);

            SaveDto entityVm = _mapper.Map<SaveDto>(entity);

            return entityVm;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetByIdSaveDto(id);
            await _repository.Delete(id);
        }

        public virtual async Task<SaveDto> GetByIdSaveDto(int id)
        {
            var entity = await _repository.GetById(id);

            SaveDto vm = _mapper.Map<SaveDto>(entity);
            return vm;
        }

        public virtual async Task<List<Dto>> GetAllDto()
        {
            var entityList = await _repository.GetAll();

            return _mapper.Map<List<Dto>>(entityList);
        }
    }
}