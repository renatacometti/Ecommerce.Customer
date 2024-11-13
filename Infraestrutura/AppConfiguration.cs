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

            //services.AddScoped<ICommonRepository<User>, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
        private static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, EnderecoService>();
            services.AddScoped<ITokenService, TokenService>();
        }

    }
}
