using Data.Auth.Seeds;
using Data.Auth.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Auth
{
    public static class AuthDataDependencyService
    {
        public static void RegisterAll(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped<IAuthSeeder, AuthSeeder>();
        }
    }
}