using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repository;
using Service.Interfaces;
using Service.Services;


namespace Infraestrutura
{
    public static class AppConfiguration
    {
        public static void ConfigureApp(IServiceCollection services, Microsoft.Extensions.Configuration.IConfigurationRoot configuration)
        {
            ConfigureRepository(services);
            ConfigureService(services);

        }

        private static void ConfigureRepository(IServiceCollection services)
        {

            services.AddScoped<ICommonRepository<Usuario>, UsuarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        }
        private static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
