using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Models
{
    public class GetTopExtractedNumbersResult
    {
       
        public ushort TopMostCount { get; set; }
        public ushort TopLeastCount { get; set; }
        public IEnumerable<KeyValuePair<string,int>> TopMostExtractedNumbers { get; set; } 
        public IEnumerable<KeyValuePair<string, int>> TopLeastExtractedNumbers { get; set; }
    }
}
