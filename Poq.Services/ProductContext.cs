using Poq.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class ProductContext
    {
        public Error Error { get;  set; }
        public IEnumerable<Product> AllProducts { get; set; }
        public IEnumerable<Product> FilteredProducts { get; set; }

        public List<string> Sizes { get; set; }=new List<string>();
        public List<string> MostCommonWords { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
       
    }
    internal static class ContextExtensions
    {
        public static ProductContext SetError(this ProductContext context, Error error)
        {
            context.Error = error;
            return context;
        }
    }
}
