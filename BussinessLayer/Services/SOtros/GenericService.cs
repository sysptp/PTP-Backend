using BussinessLayer.Interfaces.Repositories;
using AutoMapper;
using BussinessLayer.Interfaces.IOtros;

namespace BussinessLayer.Services
{
    public class GenericService<Request, Response, Model> : IGenericService<Request, Response,Model> where Request : class
            where Response : class
            where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(Request vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.Update(entity);
        }

        public virtual async Task<Response> Add(Request vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.Add(entity);

            Response entityVm = _mapper.Map<Response>(entity);

            return entityVm;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetByIdRequest(id);
            await _repository.Delete(id);
        }

        public virtual async Task<Request> GetByIdRequest(int id)
        {
            var entity = await _repository.GetById(id);

            Request vm = _mapper.Map<Request>(entity);
            return vm;
        }

        public virtual async Task<Response> GetByIdResponse(int id)
        {
            var entity = await _repository.GetById(id);

            Response vm = _mapper.Map<Response>(entity);
            return vm;
        }

        public virtual async Task<List<Response>> GetAllDto()
        {
            var entityList = await _repository.GetAll();

            return _mapper.Map<List<Response>>(entityList);
        }

        public async Task<bool> PatchUpdateAsync(int id, Dictionary<string, object> updatedProperties)
        {
            var entity = await _repository.GetById(id);

            if (entity != null)
            {
                foreach (var prop in updatedProperties)
                {
                    var propertyInfo = typeof(Model).GetProperty(prop.Key);
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(entity, prop.Value);
                    }
                }

                await _repository.Update(entity);

                return true;
            }

            return false;
        }
    }
}