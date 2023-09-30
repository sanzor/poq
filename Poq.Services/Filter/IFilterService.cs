using Poq.Models;


namespace Poq.Services;

public interface IFilterService
{
    IEnumerable<Product> FilterProducts(IEnumerable<Product> products, GetProductsParams filters);
}
