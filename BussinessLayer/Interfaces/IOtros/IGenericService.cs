namespace BussinessLayer.Interfaces.IOtros
{
    public interface IGenericService<SaveDto, Dto, Model>
        where SaveDto : class
        where Dto : class
        where Model : class
    {
        Task<SaveDto> Add(SaveDto vm);
        Task Delete(int id);
        Task<List<Dto>> GetAllDto();
        Task<SaveDto> GetByIdRequest(int id);
        Task Update(SaveDto vm, int id);
        Task<Dto> GetByIdResponse(int id);
        Task<bool> PatchUpdateAsync(int id, Dictionary<string, object> updatedProperties);
    }
}