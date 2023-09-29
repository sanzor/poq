using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Models
{
    public class GetProductsParams
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Highlight { get; set; }
        public string ? Size { get; set; }
    }
}
