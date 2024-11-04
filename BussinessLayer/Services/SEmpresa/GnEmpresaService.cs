using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.Empresas;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Empresa;

namespace BussinessLayer.Services.SEmpresa
{
    public class GnEmpresaservice : GenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>, IGnEmpresaservice
    {
        private readonly IGnEmpresaRepository _repository;
        private readonly IMapper _mapper;
        public GnEmpresaservice(IGnEmpresaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GnEmpresaResponse> GetByCodEmp(long id)
        {
           var empresa = await _repository.GetByEmpCode(id);
            return _mapper.Map<GnEmpresaResponse>(empresa);
        }

        public override async Task<bool> PatchUpdateAsync(int id, object updatedPropertiesObject)
        {
            var entity = await _repository.GetByEmpCode(id);

            if (entity != null)
            {
                var updatedProperties = ConvertObjectToDictionary(updatedPropertiesObject);

                foreach (var prop in updatedProperties)
                {
                    var entityProperty = typeof(GnEmpresa).GetProperty(prop.Key);

                    if (entityProperty != null && entityProperty.CanWrite)
                    {
                        entityProperty.SetValue(entity, prop.Value);
                    }
                }

                await _repository.Update(entity, id); 
                return true;
            }

            return false;
        }

        private Dictionary<string, object> ConvertObjectToDictionary(object obj)
        {
            return obj.GetType()
                      .GetProperties()
                      .Where(prop => prop.GetValue(obj) != null)
                      .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj)!);
        }

    }
}
