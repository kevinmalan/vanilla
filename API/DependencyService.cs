using Core.Auth.Contracts;
using Core.Auth.Services;
using Data.Auth;
using Common.Auth.Config;

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
            AuthDataDependencyService.RegisterAll(services, configuration);
        }

        public static void RegisterConfig(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordConfig>(configuration.GetSection("Password"));
            services.Configure<TokenConfig>(configuration.GetSection("Tokens"));
        }

        private static void RegisterSingletons(IServiceCollection services)
        {
        }

        private static void RegisterTransients(IServiceCollection services)
        {
        }

        private static void RegisterScoped(IServiceCollection services)
        {
            services.AddScoped<StartupFlow>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}