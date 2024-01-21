#region References
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Core.Services;
#endregion

#region Namespace
namespace Application
{
    public static class ApplicationDependencyInjections
    {
        public static void ConfigureApplicationInjections(this IServiceCollection services)
        {
            services.AddScoped<IEntityMapperService, EntityMapperService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
#endregion