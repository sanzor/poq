using FluentValidation;
using Poq.Models;

namespace Poq
{
    public class GetTopExtractedNumbersParamsValidator:AbstractValidator<GetTopExtractedNumbersDto>
    {
        public GetTopExtractedNumbersParamsValidator()
        {
            
            RuleFor(x => x.TopMostExtractedNumbersCount).NotEmpty().GreaterThan((ushort)0);
            RuleFor(x => x.TopLeastExtractedNumbersCount).NotEmpty().GreaterThan((ushort)0);
        }
    }
}
