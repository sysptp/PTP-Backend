using BussinessLayer.Enums;
using BussinessLayer.Interfaces.Services.IOtros;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.Services.Otros.AutenticationStrategy
{
    public class TokenVerificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TokenVerificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITokenVerificationStrategy CreateStrategy(int provider)
        {
            return provider switch
            {
                (int)AutenticationProvider.Google => _serviceProvider.GetRequiredService<GoogleTokenVerificationStrategy>(),
                (int)AutenticationProvider.Microsoft => _serviceProvider.GetRequiredService<MicrosoftTokenVerificationStrategy>(),
                (int)AutenticationProvider.Facebook => _serviceProvider.GetRequiredService<FacebookTokenVerificationStrategy>(),
                _ => throw new ArgumentException("Proveedor no soportado")
            };
        }
    }
}
