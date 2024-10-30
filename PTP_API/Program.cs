using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BussinessLayer.Helpers.CentroReporteriaHelpers;
using BussinessLayer.Interface.IAlmacenes;
using BussinessLayer.Interface.ICotizaciones;
using BussinessLayer.Interface.IFacturacion;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interface.IPedido;
using BussinessLayer.Interface.IProductos;
using BussinessLayer.Interface.ISuplidores;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.IBancos;
using BussinessLayer.Interfaces.IBoveda;
using BussinessLayer.Interfaces.ICaja;
using BussinessLayer.Interfaces.ICargaMasiva;
using BussinessLayer.Interfaces.ICentroReporteria;
using BussinessLayer.Interfaces.ICuentas;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.IMenu;
using BussinessLayer.Interfaces.IModulo;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Services.SALmacenes;
using BussinessLayer.Services.SAutenticacion;
using BussinessLayer.Services.SBancos;
using BussinessLayer.Services.SBoveda;
using BussinessLayer.Services.SCaja;
using BussinessLayer.Services.SCargaMasiva;
using BussinessLayer.Services.SCentroReporteria;
using BussinessLayer.Services.SCotizaciones;
using BussinessLayer.Services.SCuentas;
using BussinessLayer.Services.SEmpresa;
using BussinessLayer.Services.SFacturacion;
using BussinessLayer.Services.SGeografia;
using BussinessLayer.Services.SMenu;
using BussinessLayer.Services.SModulo;
using BussinessLayer.Services.SOtros;
using BussinessLayer.Services.SPedidos;
using BussinessLayer.Services.SProductos;
using BussinessLayer.Services.SSeguridad;
using BussinessLayer.Services.SSuplidores;
using Microsoft.EntityFrameworkCore;
using DataLayer.PDbContex;
using BussinessLayer.Helpers.CargaMasivaHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("POS_CONN")));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IRepositorySection, RepositorySection>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IReporteriaService, ReporteriaService>();
builder.Services.AddScoped<IAlmacenesService, AlmacenesService>();
builder.Services.AddScoped<IClientesService, ClienteService>();
builder.Services.AddScoped<IContactosSuplidoresService, ContactosSuplidoresService>();
builder.Services.AddScoped<ICotizacionService, CotizacionService>();
builder.Services.AddScoped<ICuentaPorPagarService, CuentasPorPagarService>();
builder.Services.AddScoped<ICuentasPorCobrar, CuentaPorCobrarService>();
builder.Services.AddScoped<IDescuentoService, DescuentoService>();
builder.Services.AddScoped<IDetalleCotizacionService, DetalleCotizacionService>();
builder.Services.AddScoped<IDetalleCuentaPorPagar, DetalleCuentaPorPagarService>();
builder.Services.AddScoped<IDetalleCuentasPorCobrar, DetalleCuentaPorCobrarService>();
builder.Services.AddScoped<IDetalleFacturacionService, DetalleFacturacionService>();
builder.Services.AddScoped<IDetalleMovimientoAlmacenService, DetalleMovimientoAlmacenService>();
builder.Services.AddScoped<IDgiiNcfService, DgiiNcfService>();
builder.Services.AddScoped<IEnvaseService, EnvaseService>();
builder.Services.AddScoped<IFacturacionService, FacturacionService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IMovimientoAlmacenService, MovimientoAlmacenService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPrecioService, PrecioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProvinciaService, ProvinciasService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ISC_EMP001service, SC_EMP001service>();
builder.Services.AddScoped<ISuplidoresService, SuplidoresService>();
builder.Services.AddScoped<ITipoMovimientoService, TipoMovimientoService>();
builder.Services.AddScoped<ITipoPagoService, TipoPagoService>();
builder.Services.AddScoped<ITipoTransaccionService, TipoTransaccionService>();
builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();
builder.Services.AddScoped<IClaimsService, ClaimsService>();
builder.Services.AddScoped<ICargaMasivaService, CargaMasivaService>();
builder.Services.AddScoped<IAperturaCierreCajasService, AperturaCierreCajasService>();
builder.Services.AddScoped<ISC_USUAR001Service, SC_USUAR001Service>();
builder.Services.AddScoped<ISC_SUC001Service, SC_SUC001Service>();
builder.Services.AddScoped<ICajaService, CajaService>();
builder.Services.AddScoped<ITipoMovimientoBancoService, TipoMovimientoBancoService>();
builder.Services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
builder.Services.AddScoped<ISC_PAIS001Service, SC_PAIS001Service>();
builder.Services.AddScoped<ISC_REG001Service, SC_REG001Service>();
builder.Services.AddScoped<ISC_PROV001Service, SC_PROV001Service>();
builder.Services.AddScoped<ISC_MUNIC001Service, SC_MUNIC001Service>();
builder.Services.AddScoped<IGn_PerfilService, Gn_PerfilService>();
builder.Services.AddScoped<ICiudades_X_PaisesService, Ciudades_X_PaisesService>();
builder.Services.AddScoped<ISC_IPSYS001Service, SC_IPSYS001Service>();
builder.Services.AddScoped<ISC_IMP001Service, SC_IMP001Service>();
builder.Services.AddScoped<ISC_HORARIO001Service, SC_HORARIO001Service>();
builder.Services.AddScoped<ISC_HORAGROUP002Service, SC_HORAGROUP002Service>();
builder.Services.AddScoped<ISC_HORA_X_USR002Service, SC_HORA_X_USR002Service>();
builder.Services.AddScoped<IMovimientoBancoesService, MovimientoBancoesService>();
builder.Services.AddScoped<IMonedasService, MonedasService>();
builder.Services.AddScoped<ICuentaBancosService, CuentaBancosService>();
builder.Services.AddScoped<IConciliacionTCTFsService, ConciliacionTCTFsService>();
builder.Services.AddScoped<IBovedaMovimientoesService, BovedaMovimientoesService>();
builder.Services.AddScoped<IBovedaCajasService, BovedaCajasService>();
builder.Services.AddScoped<IBovedaCajaDesglosesService, BovedaCajaDesglosesService>();
builder.Services.AddScoped<IBilletes_MonedaService, Billetes_MonedaService>();
builder.Services.AddScoped<IBancosService, BancosService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<DeserializadorCrearReporte>();
builder.Services.AddScoped<EntityMapper>();
builder.Services.AddScoped<CsvProcessor>();

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.UseSecurityTokenValidators = true;
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PTP API Core 8", Description = "POINT TO POINT FACTURACION RAPIDA.", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Introduce un token válido, debes autenticarte para obtener el token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();