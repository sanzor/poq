using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Models
{
    public class GetProductsResult
    {
        public IEnumerable<Product> Products { get; set; }
        public FilterObject FilterObject { get; set; }
    }
}
