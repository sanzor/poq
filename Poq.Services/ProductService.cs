using Poq.DataAccess.Contracts;
using Poq.Models;

namespace Poq.Services;

public class ProductService : IProductService
{

    private readonly IProductsDataService productDataService;
    private readonly IFilterService filterService;
    private readonly IFilterObjectService filterObjectService;

    public EitherAsync<Error, GetProductsResult> GetProductsAsync(GetProductsParams getProductsParams)
    {
        var rez =
             RightAsync<ProductContext, ProductContext>(new ProductContext { })
            .Bind(GetProductsAsync)
            .Map(context => FilterProducts(context, getProductsParams))
            .Map(context =>
            {
                context.AllProducts = context.AllProducts.Select(prod =>
                {
                    prod.Description = Highlight(prod.Description, getProductsParams.Highlight.Split(','));
                    return prod;
                });
                return context;
            })
            .Bind(context => SetFilterObject(context)
                            .ToAsync())
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
        context.FilteredProducts = filterService.FilterProducts(context.AllProducts, filterParams);
        return context;
    }
  

    private Either<ProductContext,ProductContext> SetFilterObject(ProductContext context)
    {
        return filterObjectService.CreateFilterObject(new CreateFilterObjectParams { })
            .Map(obj =>
            {
                context.FilterObject = obj;
                return context;
            })
            .MapLeft(err=>context.SetError(err) as ProductContext);
       
    }
  

    private string Highlight(string value, IEnumerable<string> highlight)
    {
        var words = value.Split(' ').Select(word => highlight.Contains(word) ? $"<em>{word}</em>" : word).Concat();
        return words;
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
