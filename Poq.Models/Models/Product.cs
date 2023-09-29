using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Models.Models
{
    public class Product
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string[] Sizes { get; set; }
        public string Description { get; set; }
    }
}
