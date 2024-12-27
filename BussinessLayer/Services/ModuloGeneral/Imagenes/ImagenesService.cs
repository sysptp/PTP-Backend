using BussinessLayer.Interfaces.Services.ModuloGeneral.Imagenes;

namespace BussinessLayer.Services.ModuloGeneral.Imagenes
{
    public class ImagenesService : IImagenesService
    {
        //private readonly PDbContext _context;
        //private readonly IMapper _mapper;
        //private readonly ITokenService _tokenService;

        //public ImagenesService(PDbContext dbContext,
        //    IMapper mapper, ITokenService tokenService)
        //{
        //    _context = dbContext;
        //    _mapper = mapper;
        //    _tokenService = tokenService;
        //}

        //public async Task<int> AddImgProduct(AddImageProductDTO model)
        //{
        //    var newDta = new Imagen
        //    {
        //        Borrado = false,
        //        FechaCreacion = DateTime.Now,
        //        UsuarioCreacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido",
        //        Descripcion = model.Descripcion,
        //        EsPrincipal = model.EsPrincipal,
        //        IdEmpresa = model.IdEmpresa,
        //        Url = model.Url
        //    };

        //    _context.Imagenes.Add(newDta);
        //    await _context.SaveChangesAsync();

        //    var newDta2 = new InvProductoImagen
        //    {
        //        ImagenId = newDta.Id,
        //        ProductoId = model.ProductoId
        //    };

        //    _context.InvProductoImagens.Add(newDta2);

        //    return newDta2.Id;
        //}

        //public async Task Delete(int Id)
        //{
        //    var data = await _context.InvProductoImagens.Include(x => x.)
        //        .FirstOrDefaultAsync(x => x.Id == Id);

        //    if (data != null)
        //    {
        //        data.Borrado = true;
        //        data.FechaModificacion = DateTime.Now;
        //        data.UsuarioModificacion = _tokenService.GetClaimValue("sub") ?? "UsuarioDesconocido";
        //        _context.Update(data);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
