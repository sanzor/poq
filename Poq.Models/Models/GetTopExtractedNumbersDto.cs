using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poq.Models
{
    public class GetTopExtractedNumbersDto
    {
        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }
        [JsonPropertyName("topLeastCount")]
        public ushort TopLeastExtractedNumbersCount { get; set; }
        [JsonPropertyName("topMostCount")]
        public ushort TopMostExtractedNumbersCount { get; set; }
    }
}
