using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Pedidos
{
    public class CreateDetallePedidoRequestValidator : AbstractValidator<CreateDetallePedidoDto>
    {
        private readonly IProductoService _productoService;
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IPedidoService _pedidoService;

        public CreateDetallePedidoRequestValidator(IProductoService productoService,
            IGnEmpresaRepository gnEmpresaRepository,
            IPedidoService pedidoService)
        {
            _productoService = productoService;
            _empresaRepository = gnEmpresaRepository;
            _pedidoService = pedidoService;

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            RuleFor(x => x.IdEmpresa)
               .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
               .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
               .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
               .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.IdPedido)
               .NotNull().WithMessage("El Id del pedido no puede ser nulo.")
               .GreaterThan(0).WithMessage("El Id del pedido debe ser mayor que 0.")
               .MustAsync(async (id, cancellation) => await OrderExits(id))
               .WithMessage("El pedido especificado no existe.");

            RuleFor(x => x.Cantidad)
                .NotEmpty().WithMessage("El campo Cantidad es obligatorio.")
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

        }

        public async Task<bool> ProductExits(int productId)
        {
            var product = await _productoService.GetProductById(productId);
            return product != null;
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> OrderExits(int id)
        {
            var company = await _pedidoService.GetById(id);
            return company != null;
        }
    }
}
