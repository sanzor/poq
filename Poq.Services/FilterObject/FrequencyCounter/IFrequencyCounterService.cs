
namespace Poq.Services;
public interface IFrequencyCounterService
{
    Dictionary<string, int> CalculateFrequencyMap(IEnumerable<string> lines);
}
