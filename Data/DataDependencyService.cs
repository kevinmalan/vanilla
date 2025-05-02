using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DataDependencyService
    {
        public static void RegisterAll(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
        }
    }
}