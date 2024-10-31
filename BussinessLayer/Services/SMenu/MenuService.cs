//using BussinessLayer.Interface.IOtros;
//using BussinessLayer.Interfaces.IAutenticacion;
//using BussinessLayer.Interfaces.IMenu;
//using BussinessLayer.Interfaces.IModulo;
//using BussinessLayer.Services.SOtros;
//using DataLayer.Models.MenuApp;
//using DataLayer.PDbContex;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace BussinessLayer.Services.SMenu
//{
//    public class MenuService : IMenuService
//    {
//        // CREADO POR MANUEL 3/10/2024
//        private readonly PDbContext _context;
//        private readonly IModuloService _moduloService;
//        private readonly IClaimsService _claimsService;

//        public MenuService(PDbContext dbContext, 
//            IClaimsService claimsService, 
//            IModuloService moduloService) 
//        {
//            _context = dbContext;
//            _claimsService = claimsService;
//            _moduloService = moduloService;
//        }

//        public async Task Add(GnMenu menu)
//        {
//            _context.GnMenu.Add(menu);

//            if(menu.Nivel == 1)
//            {
//                var modulo = new GnModulo
//                {
//                    Modulo = menu.Menu,
//                    AdicionadoPor = _claimsService.GetClaimValueByType("nombreUsuario")
//                };
//            }

//            await _context.SaveChangesAsync();
//        }

//        public async Task Delete(GnMenu menu)
//        {
//            _context.GnMenu.Remove(menu);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<GnMenu>> GetAll()
//        {
//            return await _context.GnMenu.ToListAsync();
//        }

//        public async Task<GnMenu> GetById(int id)
//        {
//            var result = await _context.GnMenu.FindAsync(id);

//            return result;
//        }

//        public async Task Update(GnMenu menu)
//        {
//            _context.Entry(menu).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        //Creado Domingo para cargar el menu refactorizado
//        public List<GnModulo> CargarMenuPorEmpresaYPerfil(ClaimsPrincipal user)
//        {
//            List<GnModulo> modulos = [];
//            var codigoEmpresa = user.FindFirst("IdEmpresa")?.Value;
//            var idPerfil = user.FindFirst("IdPerfilUsuario")?.Value;

//            if (string.IsNullOrEmpty(codigoEmpresa) || string.IsNullOrEmpty(idPerfil))
//            {
//                return modulos;
//            }

//            try
//            {
//                long codigoEmpresaLong = long.Parse(codigoEmpresa);
//                int idPerfilInt = int.Parse(idPerfil);

//                // Cargar módulos con submenús utilizando Include (Eager Loading)
//                modulos = _context.GNModulos
//                    .Include(m => m.SubMenus) // Cargar submenús asociados
//                    .Where(m => m.Borrado == false &&
//                                _context.GnEmpresaXModulos.Any(em => em.IDModulo == m.IDModulo && em.Codigo_EMP == codigoEmpresaLong && em.Borrado == false))
//                    .OrderBy(m => m.Orden)
//                    .ToList();

//                // Si es necesario filtrar los submenús para cada módulo:
//                foreach (var modulo in modulos)
//                {
//                    modulo.SubMenus = modulo.SubMenus
//                        .Where(sm => _context.GnEmpresaXPerfilXSubMenus
//                                        .Any(ps => ps.IDSubMenu == sm.IDSubMenu &&
//                                                   ps.Codigo_EMP == codigoEmpresaLong &&
//                                                   ps.IDPerfil == idPerfilInt &&
//                                                   ps.Borrado == false))
//                        .OrderBy(sm => sm.Orden)
//                        .ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//            return modulos;
//        }

//        Task<IList<GnMenu>> IGenericService<GnMenu>.GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public Task Delete(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
