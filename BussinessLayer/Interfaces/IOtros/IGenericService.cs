namespace BussinessLayer.Interfaces.IOtros
{
    public interface IGenericService<Request, Response, Model>
        where Request : class
        where Response : class
        where Model : class
    {
        Task<Response> Add(Request vm);
        Task Delete(int id);
        Task<List<Response>> GetAllDto();
        Task<Request> GetByIdRequest(int id);
        Task Update(Request vm, int id);
        Task<Response> GetByIdResponse(int id);
        Task<bool> PatchUpdateAsync(int id, object updatedPropertiesObject);
    }
}