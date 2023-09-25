using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public interface IExtractionAdapter
    {
        public Either<Error, List<Extraction>> ParseExtractions(IEnumerable<RawExtraction> extractions);
        public Either<Error, List<AggregatedExtraction>> ParseAggregatedExtractions(IEnumerable<Extraction> extractions);
    }
}
