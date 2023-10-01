using FluentAssertions;
using FluentValidation;
using Poq.Models;

namespace Poq
{
    /// <summary>
    /// We can add here in time additional valdiations on the request url paramters 
    /// </summary>
    public class GetProductsValidator:AbstractValidator<GetProductsParams>
    {
        public GetProductsValidator()
        {
            RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxPrice).GreaterThanOrEqualTo(0);
            
          
        }
    }
}
