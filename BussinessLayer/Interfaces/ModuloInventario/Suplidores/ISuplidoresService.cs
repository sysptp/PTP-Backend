using BussinessLayer.Interface.IOtros;
using DataLayer.Models.ModuloInventario.Suplidor;

public interface ISuplidoresService : IBaseService<Suplidores>
{
    // se cambio el nombre de Add a AddSuplidores por manuel
    Task AddSuplidores(Suplidores entity);
}
