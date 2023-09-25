using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public interface ITopExtractedNumbersService
    {
        Either<Error,GetTopExtractedNumbersResult> CalculateTopExtractedNumbers(CalculateTopExtractedNumbersParams topNumbersParams);
    }
}
