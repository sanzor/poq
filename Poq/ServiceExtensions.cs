using Poq.Services;
using FluentValidation;
using Serilog;
using Poq.DataAccess.Url;
using Poq.Models;

namespace Poq
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPoqServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddCoreServices(config);
            services.AddDataAccess();
            services.AddValidators();
            return services;

        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<GetProductsParams>, GetProductsValidator>();
            return services;

        }
      
    }
}
