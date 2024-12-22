using IdentityLayer.Entities;
using IdentityLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using BussinessLayer.Settings;
using Identity.Context;
using BussinessLayer.Wrappers;
using BussinessLayer.Interfaces.Services.IAccount;

namespace IdentityLayer
{
    public static class ServiceRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {

            ContextConfiguration(services, configuration);

            #region Identity
            services.AddIdentity<Usuario, GnPerfil>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
         .AddEntityFrameworkStores<IdentityContext>()
         .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var errorDescription = $"Autenticación fallida: {context.Exception.Message}";
                        var result = JsonConvert.SerializeObject(Response<string>.Unauthorized(errorDescription));
                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var result = JsonConvert.SerializeObject(Response<string>.Unauthorized("No autorizado. Token faltante o inválido."));
                            return context.Response.WriteAsync(result);
                        }
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var result = JsonConvert.SerializeObject(Response<string>.Forbidden("Acceso prohibido. No tiene los permisos necesarios."));
                        return context.Response.WriteAsync(result);
                    }
                };

            });

            #endregion
            ServiceConfiguration(services);

        }

        #region "Private methods"

        private static void ContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            #endregion
        }

        private static void ServiceConfiguration(this IServiceCollection services)
        {
            #region Services

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRoleService, RoleService>();
            #endregion
        }
        #endregion

    }
}

