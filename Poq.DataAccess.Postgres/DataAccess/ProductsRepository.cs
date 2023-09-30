
using Poq.DataAccess.Contracts;
using Poq.Models;
using System.Net.Http.Json;

namespace Poq.DataAccess.Url;


public class ProductsRepository : IProductsRepository
{

    private HttpClient client;
    private const string URL = "http://www.mocky.io/v2/5e307edf3200005d00858b49";
    public ProductsRepository(IHttpClientFactory factory)
    {
        this.client = factory.CreateClient();
    }

    public EitherAsync<Error, IEnumerable<Product>> GetProductsAsync()
    {
        var rez = TryAsync(async () =>
        {

            var response = await this.client.GetFromJsonAsync<ProductResponse>(URL);
            if (response == null || response.Products==null)
            {
                throw new ValueIsNullException("Returned product list is null");
            }
            return response.Products.AsEnumerable();
        }).ToEither();
        return rez;
    }
}
