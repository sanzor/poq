
namespace Poq.Services;
public interface IFrequencyCounterService
{
    SortedDictionary<string, int> CalculateFrequencyMap(IEnumerable<string> lines);
}
