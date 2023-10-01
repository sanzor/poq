using FluentAssertions;
using Newtonsoft.Json;
using Poq.Models;
using System.Text.RegularExpressions;
using Xunit;

namespace Poq.Tests
{
    /// <summary>
    /// Class for integration tests , testing the app end to end 
    /// </summary>
    [Collection("IntegrationTests")]
    public class EndToEndTests
    {
        public async Task<IEnumerable<Product>> ReadFromFileDataAsync()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Repos", "stubs", "Mocky.json");
            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            var response = JsonConvert.DeserializeObject<ProductResponse>(json) ?? new ProductResponse();
            return response.Products;
        }
        private HttpClient _httpClient;
        [Fact]
        public async void DoesNotThrow()
        {
            Func<Task> func = () =>
            {
                return _httpClient.GetAsync($"/get-products");
            };
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CanSuccesfullyDoRequest()
        {
            var resp = await _httpClient.GetAsync($"/get-products");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanRetrieveProductResponse()
        {
            var resp = await _httpClient.GetAsync($"/get-products");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task MostCommonWordsAreSorted()
        {
            var resp = await _httpClient.GetAsync($"/get-products");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Should().NotBeNull();
            productResponse.FilterObject.MostCommonWords.Should().NotBeNull();

        }

        [Fact]
        public async Task CanSendWithMinPrice()
        {


            var resp = await _httpClient.GetAsync($"/get-products?minprice=5");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Should().NotBeNull();

        }
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public async Task CanFilterWithMinPrice(int minPrice)
        {
            var realDataProducts =
               await ReadFromFileDataAsync();
           
            var realMaxPrice = realDataProducts.Max(x => x.Price);
            if (minPrice > realMaxPrice)
            {
                return;
            }
            var resp = await _httpClient.GetAsync($"/get-products?minprice=5");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Products.Any(x => x.Price >= minPrice)
                .Should().BeTrue();

            Assert.True(productResponse.Products.Count() > 0);
            
        }

        [Theory]
        [InlineData("s,xs,y")]
        public async Task CanFilterWithSize(string size)
        {
            var realDataProducts =
              await ReadFromFileDataAsync();
            var targetProducts = realDataProducts.Where(x => x.Sizes.Contains(size));
            var resp = await _httpClient.GetAsync($"/get-products?minprice=5");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            
        }

        [Theory]
        [InlineData(10)]
        [InlineData(13)]
        [InlineData(24)]
        [InlineData(25)]
        [InlineData(26)]

        public async Task CanFilterWithMaxPrice(int maxPrice)
        {
            var realDataProducts =
               await ReadFromFileDataAsync();
            var realMinPrice = realDataProducts.Min(x => x.Price);
            if (maxPrice < realMinPrice)
            {
                return;
            }
            var resp = await _httpClient.GetAsync($"/get-products?maxprice=5");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Products.Any(x => x.Price <= maxPrice).Should().BeTrue();
            Assert.True(productResponse.Products.Count() > 0);
        }

        [Theory]
        [InlineData(10, 11)]
        [InlineData(13, 14)]
        [InlineData(16, 19)]
        [InlineData(11, 24)]
        [InlineData(22, 24)]

        public async Task CanFilterWithMinAndMaxPrice(int minPrice, int maxPrice)
        {
            var realDataProducts =
               await ReadFromFileDataAsync();
            var realMinPrice = realDataProducts.Min(x => x.Price);
            var realMaxPrice = realDataProducts.Max(x => x.Price);
            if (minPrice > maxPrice)
            {
                return;
            }
            if(maxPrice<realMinPrice || minPrice > realMaxPrice)
            {
                return;
            }
            var resp = await _httpClient.GetAsync($"/get-products?maxprice=5");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Products.Any(x =>
                minPrice <= x.Price && 
                x.Price <= maxPrice)
                .Should().BeTrue();
            Assert.True(productResponse.Products.Count() > 0);
        }

        [Theory]
        [InlineData("green,blue")]
        [InlineData("blue")]
        [InlineData("green")]
        public async Task CanUseHighlight(string highlightsString) {
            var highlights = highlightsString.Split(',');
            var resp = await _httpClient.GetAsync($"/get-products?highlight={highlightsString}");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Should().NotBeNull();
            var descriptions = productResponse.Products.Select(x => x.Description);
            var words =descriptions.SelectMany(x=>Regex.Matches(x, "(?<=<em>)(.*?)(?=</em>)"));
            if (words.Count() == 0)
            {
                return;
            }
            words.Any(w => highlights.Contains(w.Value)).Should().BeTrue();
        }

        [Theory]
        [InlineData("green,blue",11,15)]
        [InlineData("green",11,22)]
        public async Task CanUseMinMaxHightlight(string highlightsString,int minPrice,int maxPrice)
        {
            var realDataProducts =
              await ReadFromFileDataAsync();
            var realMinPrice = realDataProducts.Min(x => x.Price);
            var realMaxPrice = realDataProducts.Max(x => x.Price);
            if (minPrice > maxPrice)
            {
                return;
            }
            if (maxPrice < realMinPrice || minPrice > realMaxPrice)
            {
                return;
            }

            var highlights = highlightsString.Split(',');
            var resp = await _httpClient
                .GetAsync($"/get-products?minprice={minPrice}" +
                $"&maxprice={maxPrice}" +
                $"&highlight={highlightsString}");
            resp.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var dataString = await resp.Content.ReadAsStringAsync();
            var productResponse = JsonConvert.DeserializeObject<GetProductsResult>(dataString);
            productResponse.Should().NotBeNull();
           
            productResponse.Products.Any(x =>
                minPrice <= x.Price &&
                x.Price <= maxPrice)
                .Should().BeTrue();
            Assert.True(productResponse.Products.Count() > 0);
            var descriptions = productResponse.Products.Select(x => x.Description);
            var words = descriptions.SelectMany(x => Regex.Matches(x, "(?<=<em>)(.*?)(?=</em>)"));
            words.Any(w => highlights.Contains(w.Value)).Should().BeTrue();
        }
        public EndToEndTests(PoqApplicationFactory factory)
        {
            _httpClient = factory.Client;
        }
    }
}