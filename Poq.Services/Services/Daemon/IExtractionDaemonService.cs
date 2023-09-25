using LanguageExt;
using LanguageExt.Common;

namespace Poq.Services
{
    public interface IExtractionDaemonService
    {
        EitherAsync<Error, Unit> InsertWinnersAsync();
    }
}
