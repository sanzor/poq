
using Poq.Models;
using LanguageExt;
using LanguageExt.Common;

namespace Poq.DataAccess.Contracts
{
    public interface IExtractionRepository
    {
        EitherAsync<Error, Unit> InsertExtractionsAsync(List<Extraction> extraction);

        EitherAsync<Error, IEnumerable<Extraction>> GetExtractionsWithDateFilterAsync(DateOnly date);

        EitherAsync<Error, GetTopExtractedNumbersResult> GetTopExtractedNumbersForDateAsync(GetTopExtractedNumbersParams topExtractedParams);

    }
}
