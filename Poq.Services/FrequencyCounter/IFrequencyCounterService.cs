using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services.FrequencyCounter
{
    public interface IFrequencyCounterService
    {
        SortedDictionary<string, int> CalculateFrequencyMap(IEnumerable<string> lines);
    }
}
