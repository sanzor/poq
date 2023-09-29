using Poq.Services;
using Poq.DataAccess.Postgres;
using FluentValidation;
using Serilog;

namespace Poq
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddGetsBetsServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddCoreServices(config);
            services.AddDataAccess();
            services.AddValidators();
            return services;

        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator, GetTopExtractedNumbersParamsValidator>();
            return services;

        }
      
    }
}
