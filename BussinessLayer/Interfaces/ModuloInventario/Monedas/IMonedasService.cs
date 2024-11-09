using DataLayer.Models.ModuloInventario.Monedas;

public interface IMonedasService
{
    Task Add(Moneda model);

    Task Delete(Moneda model);

    Task<List<Moneda>> GetAll();

    Task<Moneda> GetById(int id);

    Task Update(Moneda model);
}

