using Poq.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Poq.Services
{
    public static class DIExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IFilterObjectService, FilterObjectService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IFilterService, FilterService>();
            services.AddSingleton<IFrequencyCounterService,FrequencyCounterService>();
            return services;
        }
     
    }
}
