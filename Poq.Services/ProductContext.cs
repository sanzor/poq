using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    /// <summary>
    /// Class for holding state within our flow
    /// We set up each of its fields sequentially (according to the task)
    /// </summary>
    internal class ProductContext:BaseContext
    {
        /// <summary>
        /// Data retrieved from the database ( provided url) 
        /// and also with highlighted descriptions (by the end of the flow)
        /// </summary>
        public IEnumerable<Product> AllProducts { get; set; }

        /// <summary>
        /// Data filtered according to the url parameters (minprice,maxprice,sizes)
        /// </summary>
        public IEnumerable<Product> FilteredProducts { get; set; }

        /// <summary>
        /// Object for task 3 c 
        /// </summary>
        public FilterObject FilterObject { get; set; }
        
       
    }
   
}
