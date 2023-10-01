using Poq.Models;
using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class FilterService : IFilterService
    {
       

        public IEnumerable<Product> FilterProducts(IEnumerable<Product> products, FilterParams filters)
        {
            var rez = (filters.MinPrice, filters.MaxPrice) switch
            {
                (null, null) => products,
                (int minprice, null) => products.Where(prod => prod.Price >= filters.MinPrice),
                (null, int maxprice) => products.Where(prod => prod.Price <= filters.MaxPrice),
                (int minprice, int maxprice) => products
                    .Where(prod => filters.MinPrice <= prod.Price && prod.Price <= filters.MaxPrice)
            };
            if (string.IsNullOrEmpty(filters.Size)){
                return rez;
            }
            var sizeFiltered = rez.Where(prod => prod.Sizes.Contains(filters.Size));
            return sizeFiltered;
          

        }
    }
}
