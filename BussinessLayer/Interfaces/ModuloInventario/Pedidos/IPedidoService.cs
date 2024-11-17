using BussinessLayer.DTOs.ModuloInventario.Pedidos;

public interface IPedidoService 
{
    // Obtener data por su id
    Task<ViewOrderDto> GetById(int id);

    // Obtener data por su empresa
    Task<List<ViewOrderDto>> GetByCompany(int id);

    // Crear un nuevo 
    Task<int?> Create(CreateOrderDto create);

    // Editar existente
    Task Edit(EditOrderDto edit);

    // Servicio para eliminar por id unico
    Task DeleteById(int id);

}
