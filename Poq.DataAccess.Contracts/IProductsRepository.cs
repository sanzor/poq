
using Poq.Models;
using LanguageExt;
using LanguageExt.Common;
using Poq.Models.Models;

namespace Poq.DataAccess.Contracts
{
    public interface IProductsRepository
    {
        EitherAsync<Error,IEnumerable<Product>> GetProductsAsync();
       

    }
}
