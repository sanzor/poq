using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public class FrequencyCounterService : IFrequencyCounterService
    {
        public Dictionary<string, int> CalculateFrequencyMap(IEnumerable<string> descriptions)
        {
            var frequencyMap=new Dictionary<string, int>();
            foreach (string description in descriptions)
            {
                var words=description.Split(' ');
                foreach (var item in words)
                {
                    if(!frequencyMap.TryGetValue(item,out int _))
                    {
                        frequencyMap.Add(item, 1);
                    }
                    else
                    {
                        frequencyMap[item]++;
                    }
                }
            }
            var sortedDict =
                frequencyMap.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            return sortedDict;
        }
    }
}
