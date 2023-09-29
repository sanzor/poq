using Poq.Models;
using LanguageExt;
using LanguageExt.Common;
using Poq.Models.Models;

namespace Poq.DataAccess.Contracts
{
    public interface IProductsDataService
    {
        EitherAsync<Error, IEnumerable<Product>> GetProductsAsync();
       
       
    }
}
