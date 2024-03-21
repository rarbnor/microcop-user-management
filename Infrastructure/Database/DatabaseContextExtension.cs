using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database
{
	public static class DatabaseContextExtension
    {
        public static IServiceCollection RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)), ServiceLifetime.Transient);

            return services;
        }
    }
}

