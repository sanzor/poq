using Poq.DataAccess.Common;
using Poq.DataAccess.Contracts;
using Poq.Models;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnitsOfMeasure;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Poq.Models.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Poq.DataAccess.Postgres
{

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

                var products = await this.client.GetFromJsonAsync<IEnumerable<Product>>(URL);
                if (products == null)
                {
                    throw new ValueIsNullException("Returned product list is null");
                }
                return products;
            }).ToEither();
            return rez;
        }
    }
}
