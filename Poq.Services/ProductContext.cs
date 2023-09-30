using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class ProductContext:BaseContext
    {
        public IEnumerable<Product> AllProducts { get; set; }

        public IEnumerable<Product> FilteredProducts { get; set; }
        public FilterObject FilterObject { get; set; }
        
       
    }
   
}
