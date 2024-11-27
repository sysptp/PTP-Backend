using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Monedas;

public interface IMonedasService
{
    Task Add(CreateCurrencyDTO model);

    Task Delete(int model);

    Task<List<ViewCurrencyDTO>> GetAll();

    Task<ViewCurrencyDTO> GetById(int id);

    Task Update(EditCurrencyDTO model);
}

