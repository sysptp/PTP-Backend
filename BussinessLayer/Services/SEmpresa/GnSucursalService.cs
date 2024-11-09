using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Empresa;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SEmpresa
{

    // CREADO POR DOMINGO 11/02/2024
    public class GnSucursalService : GenericService<GnSucursalRequest, GnSucursalResponse, GnSucursal>, IGnSucursalService
    {
        private readonly IGnSucursalRepository _repository;
        private readonly IMapper _mapper;
        public GnSucursalService(IGnSucursalRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GnSucursalResponse> GetBySucursalCode(long? id)
        {
            return _mapper.Map<GnSucursalResponse>(await _repository.GetBySucursalCode(id));
        }

        //public async Task<List<GnSucursal>> GetAllIndex()
        //{
        //    return await _context.SC_SUC001
        //        .Include(s => s.GnEmpresa)
        //        .ToListAsync();
        //}

    }
}
