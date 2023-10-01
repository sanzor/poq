using FluentAssertions;
using Poq.Models;
using Poq.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Tests
{
    /// <summary>
    /// Class for more fine grained tests , usually using mocks 
    /// We test the services here  
    /// </summary>
    public class CoreTests
    {
        [Fact]
        public void CanCalculateTheFrequencyMap()
        {
            var data = new List<string>
            {
                "i am",
                "i",
                "am",
                "32 years old",
                "i"
            };
            IFrequencyCounterService counterService = new FrequencyCounterService();
            var result=counterService.CalculateFrequencyMap(data);
            var topFrequency = result.First();
            topFrequency.Key.Should().Be("i");
            topFrequency.Value.Should().Be(3);
        }
        [Fact]
        public void CanCreateTheFilterObject()
        {
            int length = 11;
            var products = Enumerable.Range(0, length).Select((x,index) => new Product
            {
                Price = x,
                Sizes=new string[] { x.ToString() },
                Description=string.Join(" ",Enumerable.Repeat(x,length-x))
            });
            IFilterObjectService filterObject = new FilterObjectService(new FrequencyCounterService());
            filterObject.CreateFilterObject(new CreateFilterObjectParams
            {
                AllProducts = products
            }).Match(filterObject =>
            {
                var mostFoundWord = filterObject.MostCommonWords.FirstOrDefault();
                Assert.Equal(mostFoundWord, (length - 5-1).ToString());
            }, err =>
            {
                throw new Exception(err.Message);
            });
        }
    }
}
