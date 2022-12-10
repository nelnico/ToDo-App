using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Services;
using TodoApp.Data;

namespace TodoApp.Api.Common.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            var policyName = config.GetValue<string>("CorsPolicyName");
            services.AddCors();

            services.AddCors(opt =>
            {
                opt.AddPolicy(policyName, corsPolicyBuilder =>
                {
                    corsPolicyBuilder
                        .WithOrigins("https://localhost:4200")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            return services;
        }
    }
}
