using Poq.Models;

namespace Poq.DataAccess.Contracts;

public interface IProductsDataService
{
    EitherAsync<Error, IEnumerable<Product>> GetProductsAsync();
}
