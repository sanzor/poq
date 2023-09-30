
using Poq.DataAccess.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Poq.DataAccess.Url;

public static class DIExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<IProductsDataService, ProductsDataService>();
        services.AddSingleton<IProductsRepository, ProductsRepository>();
        return services;
    }
}
