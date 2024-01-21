using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace IAM.Infrastructure
{
    public static class InfrastructureInjections
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

            using (var dbContext = new ApplicationDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
