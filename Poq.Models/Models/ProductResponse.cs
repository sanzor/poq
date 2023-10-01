using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poq.Models
{
    /// <summary>
    /// Response from the given url
    /// </summary>
    public class ProductResponse
    {
        [JsonPropertyName("products")]
        public List<Product> Products { get; set;}
    }
}
