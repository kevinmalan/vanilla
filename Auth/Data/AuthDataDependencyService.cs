using Auth.Data.Seeds;
using Auth.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auth.Data.Repositories;
using Auth.Data.Services;

namespace Auth.Data
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