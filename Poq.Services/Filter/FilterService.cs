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
       

        public IEnumerable<Product> FilterProducts(IEnumerable<Product> products, GetProductsParams filters)
        {
            var filteredProducts = products.Where(x =>
                (filters.MinPrice.HasValue ? x.Price > filters.MinPrice.Value : true) &&
                (filters.MaxPrice.HasValue ? x.Price <= filters.MaxPrice.Value : true) &&
                (!string.IsNullOrEmpty(filters.Size)) ? x.Sizes.Contains(filters.Size) : true);
            return filteredProducts;

        }
    }
}
