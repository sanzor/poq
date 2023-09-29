using Poq.Models;
using Poq.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public interface IFilterService
    {
        IEnumerable<Product> FilterProducts(IEnumerable<Product> products, GetProductsParams filters);
    }
}
