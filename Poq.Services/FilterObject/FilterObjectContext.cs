using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class FilterObjectContext:BaseContext
    {
        //Input
        public IEnumerable<Product> Products { get; set; }

       
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> MostCommonWords { get; set; } = new List<string>();

    }
}
