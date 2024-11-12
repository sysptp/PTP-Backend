

using AutoMapper;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;
using Microsoft.Identity.Client;

namespace BussinessLayer.Repository.REmpresa
{
    public class GnSucursalRepository : GenericRepository<GnSucursal>, IGnSucursalRepository
    {
        private readonly PDbContext _dbContext;

        public GnSucursalRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
        }

    }
}
