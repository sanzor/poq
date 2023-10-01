using Poq.DataAccess.Contracts;
using Poq.Models;

namespace Poq.Services;

/// <summary>
/// Class containing all the flow of our request
/// We are using LanguageExt a functional library 
/// </summary>
public class ProductService : IProductService
{

    private readonly IProductsDataService productDataService;
    private readonly IFilterService filterService;
    private readonly IFilterObjectService filterObjectService;
    private const string HIGHLIGHT_TAG = "em";

    /// <summary>
    /// Method containing all the flow 
    /// First we retrieve the products from the database (url)
    /// Then we filter the products according to the url filters (minprice,maxprice,size)
    /// We then update all the product descriptions according to the highlight paramter in the url
    /// Then we create the filterobject (task 3c)
    /// Finally we wrap it up in an object
    /// </summary>
    /// <param name="getProductsParams"></param>
    /// <returns></returns>
    public EitherAsync<Error, GetProductsResult> GetFilteredProductsAsync(GetProductsParams getProductsParams)
    {
        var rez =
             RightAsync<ProductContext, ProductContext>(new ProductContext { })
            .Bind(GetProductsAsync)
            .Map(context => FilterProducts(context, getProductsParams))
            .Map(context =>
            {
                context.AllProducts = from product in context.AllProducts
                                      select HighlightProduct(product, getProductsParams.Highlight);
                return context;
            })
            .Bind(SetupFilterObject)
            .Map(ct => new GetProductsResult
            {
                Products = ct.FilteredProducts,
                FilterObject = ct.FilterObject
            })
           .MapLeft(ct=>ct.Error);
        return rez;

    }
    private EitherAsync<ProductContext, ProductContext> GetProductsAsync(ProductContext context)
    {
        return productDataService
                .GetProductsAsync()
                .Map(products =>
                {
                    context.AllProducts = products;
                    return context;
                })
                .MapLeft(err => context.SetError(err) as ProductContext);
    }
    private ProductContext FilterProducts(ProductContext context, GetProductsParams filterParams)
    {
        context.FilteredProducts = 
            filterService.FilterProducts(context.AllProducts, new FilterParams
            {
                MaxPrice= filterParams.MaxPrice,
                MinPrice= filterParams.MinPrice,
                Size=filterParams.Size,
            });
        return context;
    }
  

    private EitherAsync<ProductContext,ProductContext> SetupFilterObject(ProductContext context)
    {
        return filterObjectService.CreateFilterObject(new CreateFilterObjectParams { 
                AllProducts=context.AllProducts})
            .Map(obj =>
            {
                context.FilterObject = obj;
                return context;
            })
            .MapLeft(err=>context.SetError(err) as ProductContext)
            .ToAsync();
       
    }
  

  
    private Product HighlightProduct(Product product,string highlightsString)
    {
        var highlights =
                   string.IsNullOrEmpty(highlightsString) ?
                   Enumerable.Empty<string>() :
                   highlightsString.Split(',');

        product.Description = HighlightProductDescription(product.Description, highlights);
        return product;
    }
    private string HighlightProductDescription(string productDescription, IEnumerable<string> highlight)
    {
        var descriptionWords = productDescription.Split(' ')
            .Select(descriptionWord =>
                highlight.Contains(descriptionWord) ? 
                $"<{HIGHLIGHT_TAG}>{descriptionWord}</{HIGHLIGHT_TAG}>" :
                descriptionWord);

        return string.Join(' ', descriptionWords);
    }
    public ProductService(
        IProductsDataService extractionService,
        IFilterService filterService,
        IFilterObjectService filterObjectService)
    {
        this.productDataService = extractionService ??
            throw new ArgumentNullException(nameof(extractionService));
        this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
        this.filterObjectService = filterObjectService??throw new ArgumentNullException(nameof(filterObjectService));
    }




}
