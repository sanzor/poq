using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Tests
{
    [CollectionDefinition("IntegrationTests")]
    public class PoqApplicationFactory:WebApplicationFactory<IAmApiMarker>,ICollectionFixture<PoqApplicationFactory>
    {
        public HttpClient Client { get; }
        public PoqApplicationFactory()
        {
            Client = CreateClient();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var productsRepoMock = MockRepositoryFactory.GetProductsRepository();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(productsRepoMock.Object);
            });
        }
    }
}
