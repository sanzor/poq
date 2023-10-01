using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poq.Models
{
    [Serializable]
    public class GetProductsFilterParamsDto
    {
       public int? MinPrice { get; set; }
        public int?MaxPrice { get; set; }
        public string? Highlight { get; set; }
        public string? Size { get; set; }
      
    }
}
