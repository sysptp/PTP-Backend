namespace BussinessLayer.Interfaces.Services.IOtros
{
    public interface IGenericService<Request, Response, Model>
        where Request : class
        where Response : class
        where Model : class
    {
        Task<Response> Add(Request vm);
        Task Delete(int id);
        Task Delete(object id);
        Task<List<Response>> GetAllDto();
        Task<Request> GetByIdRequest(int id);
        Task<Request> GetByIdRequest(object id);
        Task Update(Request vm, int id);
        Task Update(Request vm, object id);
        Task<Response> GetByIdResponse(int id);
        Task<Response> GetByIdResponse(object id);
    }
}