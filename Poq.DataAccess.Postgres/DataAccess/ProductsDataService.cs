using Poq.DataAccess.Contracts;
using Poq.Models;
using LanguageExt;
using LanguageExt.Common;
using Poq.Models.Models;

namespace Poq
{
    public class ProductsDataService : IProductsDataService
    {
      
        public IProductsRepository _productsRepository { get; }

        public EitherAsync<Error, IEnumerable<Product>> GetProductsAsync()
        {
            return _productsRepository.GetProductsAsync();
        }
        public ProductsDataService(IProductsRepository productsRepository)
        {
                _productsRepository= productsRepository??throw new ArgumentNullException(nameof(productsRepository));
        }
    }
}
