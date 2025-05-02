using Core.Auth.Config;
using Core.Auth.Contracts;
using Core.Auth.Services;
using Data;

namespace API
{
    public class DependencyService
    {
        public static void RegisterAll(IServiceCollection services, IConfiguration configuration)
        {
            RegisterConfig(services, configuration);
            RegisterSingletons(services);
            RegisterTransients(services);
            RegisterScoped(services);
            DataDependencyService.RegisterAll(services, configuration);
        }

        public static void RegisterConfig(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordConfig>(configuration.GetSection("Password"));
        }

        private static void RegisterSingletons(IServiceCollection services)
        {
            services.AddSingleton<IPasswordService, PasswordService>();
        }

        private static void RegisterTransients(IServiceCollection services)
        {
        }

        private static void RegisterScoped(IServiceCollection services)
        {
        }
    }
}