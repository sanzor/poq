using Poq.Models;
using Poq.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Poq
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private Serilog.ILogger _logger = Log.ForContext<ProductController>();
       
        public IProductService _productService { get; }

        // GET: api/<ValuesController1>
        [HttpGet]
        [Route("/get-products")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductsAsync(
            [FromQuery(Name ="minprice")]int? minPrice,
            [FromQuery(Name="maxprice")] int? maxPrice,
            [FromQuery(Name ="highlight")] string?highlight)
        {
            if (!ModelState.IsValid)
            {
                var modelErrrors = GetModelStateErrors(ModelState);
                return BadRequest(modelErrrors);
            }
            var  result = await Adapter
                .Adapt(new GetProductsFilterParamsDto {Highlight=highlight,MaxPrice=maxPrice,MinPrice=minPrice })
                .ToAsync()
            
            .Bind(_productService.GetProductsAsync)
            .Match(ok =>
            {
                return StatusCode(200, ok);
            }, err =>
            {
                return StatusCode(500, err.Message);
            });
            return result;
            
        }

       

       
        private static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Keys
                .SelectMany(key => modelState[key].Errors)
                .Select(error => error.Exception != null ? error.Exception.Message : error.ErrorMessage)
                .ToList();
            return string.Join(Environment.NewLine, errorList);
        }
        

        public ProductController(IProductService productService)
        {
            _productService = productService??throw new ArgumentNullException(nameof(productService));
        }
    }
}
