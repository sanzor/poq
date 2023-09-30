using FluentAssertions;

namespace Poq.Tests
{
    [Collection("IntegrationTests")]
    public class EndToEndTests
    {
        private HttpClient _httpClient;
        [Fact]
        public async void CanGetProductsAsync()
        {
            Func<Task> func = () =>
            {
                return _httpClient.GetAsync($"/get-products");
            };
            await func.Should().NotThrowAsync();
        }
        public EndToEndTests(PoqApplicationFactory factory)
        {
            _httpClient = factory.Client;
        }
    }
}