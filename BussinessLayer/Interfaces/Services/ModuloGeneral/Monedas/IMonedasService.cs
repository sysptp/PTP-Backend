using BussinessLayer.DTOs.ModuloGeneral.Monedas;

public interface IMonedasService
{
    Task<int> Add(CreateCurrencyDTO model);

    Task Delete(int model);

    Task<List<ViewCurrencyDTO>> GetAll();

    Task<ViewCurrencyDTO> GetById(int id);

    Task Update(EditCurrencyDTO model);

    Task<List<ViewCurrencyDTO>> GetByCompany(int idEmpresa);
}

