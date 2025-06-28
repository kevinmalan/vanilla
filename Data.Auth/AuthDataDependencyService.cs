using Data.Auth.Seeds;
using Data.Auth.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data.Auth.Repositories;
using Data.Auth.Services;

namespace Data.Auth
{
    public static class AuthDataDependencyService
    {
        public static void RegisterAll(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            // Scoped
            services.AddScoped<IAuthSeeder, AuthSeeder>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            // Singleton
            services.AddSingleton<IPasswordService, PasswordService>();
        }
    }
}