using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public class FrequencyCounterService : IFrequencyCounterService
    {
        public SortedDictionary<string, int> CalculateFrequencyMap(IEnumerable<string> descriptions)
        {
            var sortedDictionary=new SortedDictionary<string, int>();
            foreach (string description in descriptions)
            {
                var words=description.Split(' ');
                foreach (var item in words)
                {
                    if(!sortedDictionary.TryGetValue(item,out int _))
                    {
                        sortedDictionary.Add(item, 1);
                    }
                    else
                    {
                        sortedDictionary[item]++;
                    }
                }
            }
            return sortedDictionary;
        }
    }
}
