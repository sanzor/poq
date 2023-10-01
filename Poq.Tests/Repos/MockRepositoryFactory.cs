using Moq;
using Newtonsoft.Json;
using Poq.DataAccess.Contracts;
using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Tests
{
    /// <summary>
    /// We set up a data source from a local file , so that we dont depend on making calls to the specific url
    /// The file in Repos/Stubs/Mocky.json is the exact response of the url provided in thhe task
    /// </summary>
    internal static class MockRepositoryFactory
    {
       public static Mock<IProductsRepository> GetProductsRepository()
       {
            var mock = new Mock<IProductsRepository>();
            var products = ReadFromJson("Mocky.json");
            mock.Setup(x => x.GetProductsAsync())
                .Returns(RightAsync<Error, IEnumerable<Product>>(products));
            return mock;
       }
        private static List<Product> ReadFromJson(string file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Repos", "stubs", file);
            using var reader=new StreamReader(path);
            var json=reader.ReadToEnd();
            var response= JsonConvert.DeserializeObject<ProductResponse>(json) ?? new ProductResponse();
            return response.Products;
        }
    }
}
