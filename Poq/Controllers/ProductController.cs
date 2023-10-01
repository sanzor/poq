using Poq.Models;
using Poq.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using FluentValidation;

namespace Poq;

/// <summary>
/// To put authorization on controller !
/// </summary>
/// 
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private Serilog.ILogger _logger = Log.ForContext<ProductController>();

    public readonly IProductService _productService;
    private readonly IValidator<GetProductsParams> _validator;

    // GET: api/<ValuesController1>
    /// <summary>
    /// We retrieve the url arguments , construct a dto , validate it according to rules\n
    /// With the input object we then proceed towards our flow\n
    /// The result of the flow is matched if it is an ok or an error and given an according HTTP status code
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="highlight"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("/get-products")]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductsAsync(

        [FromQuery(Name = "minprice")] int? minPrice,
        [FromQuery(Name = "maxprice")] int? maxPrice,
        [FromQuery(Name = "highlight")] string? highlight,
        [FromQuery(Name ="size")] string? size)
    {

        var result = await Adapter
            .Adapt(new GetProductsFilterParamsDto
            {
                Highlight = highlight,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                Size=size
                
            })
            .Bind(getProductParams =>
            {
                var rez = _validator.Validate(getProductParams);
                if (!rez.IsValid)
                {
                    var errorMessage = string.Join("\n", rez.Errors.Select(x => x.ErrorMessage));
                    return Left<Error, GetProductsParams>
                    (Error.New("Validation Error:\n" + errorMessage));
                }
                return Right<Error, GetProductsParams>(getProductParams);
            })
        .ToAsync()
        .Bind(_productService.GetFilteredProductsAsync)
        .Match(ok =>
        {
            return StatusCode(200, ok);
        }, err =>
        {
            _logger.Error(err.Message);
            if (err.Message.StartsWith("Validation"))
            {

                return StatusCode(400, err.Message);
            }
            return StatusCode(500, err.Message);
        });
        return result;

    }

    public ProductController(IProductService productService, IValidator<GetProductsParams> validator)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
}
