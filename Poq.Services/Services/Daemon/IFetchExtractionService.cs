using Poq.Models;

namespace Poq.Services
{
    public interface IFetchExtractionService
    {
        EitherAsync<Error, List<RawExtraction>> GetExtractionFromSourceAsync();
    }
}