using Poq.DataAccess.Contracts;
using Poq.Models;
using Poq.Models.Models;
using Poq.Services.FrequencyCounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductsDataService productDataService;
        private readonly IFrequencyCounterService frequencyCounterService;
        private readonly IFilterService filterService;

        private const int SKIP_MOST_COMMON_WORDS_COUNT = 5;
        private const int TAKE_MOST_COMMON_WORDS_COUNT = 10;

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
                                .ToAsync());
               
               

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
                    .MapLeft(err => context.SetError(err));
        }
        private ProductContext FilterProducts(ProductContext context, GetProductsParams filterParams)
        {
            context.FilteredProducts = filterService.FilterProducts(context.AllProducts, filterParams);
            return context;
        }
        private ProductContext GetSizes(ProductContext context)
        {
            context.Sizes = context.AllProducts.SelectMany(x => x.Sizes).ToHashSet().ToList();
            return context;
        }

        private Either<ProductContext,ProductContext> SetFilterObject(ProductContext context)
        {
            var result= Right<ProductContext, ProductContext>(context)
           .Map(GetSizes)
              .Bind(c => GetMostCommonWords(c))
              .Map(context =>
              {
                  (double minPrice, double maxPrice) = GetMinMaxPrice(context.AllProducts);
                  context.MinPrice = minPrice;
                  context.MaxPrice = maxPrice;
                  return context;
              });
            return result;
        }
        private Either<ProductContext, ProductContext> GetMostCommonWords(ProductContext context)
        {
            var descriptions = context.AllProducts.Select(x => x.Description);
            var frequencyMap = frequencyCounterService.CalculateFrequencyMap(descriptions);
            if (frequencyMap.Count < TAKE_MOST_COMMON_WORDS_COUNT)
            {
                var error = Error.New("Invalid resulting common words count");
                return Left<ProductContext, ProductContext>(context.SetError(error));
            }
            context.MostCommonWords = frequencyMap
                .Keys
                .Skip(SKIP_MOST_COMMON_WORDS_COUNT)
                .Take(TAKE_MOST_COMMON_WORDS_COUNT).ToList();
            return Right<ProductContext, ProductContext>(context);
        }
        private (double minPrice, double maxPrice) GetMinMaxPrice(IEnumerable<Product> products)
        {
            return ((products.Min(x => x.Price), products.Max(x => x.Price)));
        }


        private string Highlight(string value, IEnumerable<string> highlight)
        {
            var words = value.Split(' ').Select(word => highlight.Contains(word) ? $"<em>{word}</em>" : word).Concat();
            return words;
        }

        public ProductService(
            IProductsDataService extractionService,
            IFrequencyCounterService frequencyCounterService,
            IFilterService filterService)
        {
            this.productDataService = extractionService ??
                throw new ArgumentNullException(nameof(extractionService));
            this.frequencyCounterService = frequencyCounterService ??
                throw new ArgumentNullException(nameof(frequencyCounterService));
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
        }




    }
}
