using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Models
{
    /// <summary>
    /// Object containing data for task 3c
    /// </summary>
    public class FilterObject
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> MostCommonWords { get; set; } = new List<string>();
    }
}
