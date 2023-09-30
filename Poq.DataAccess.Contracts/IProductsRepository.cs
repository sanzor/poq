
using Poq.Models;

namespace Poq.DataAccess.Contracts;

public interface IProductsRepository
{
    EitherAsync<Error,IEnumerable<Product>> GetProductsAsync();
   

}
