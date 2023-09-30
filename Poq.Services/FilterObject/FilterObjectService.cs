using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Poq.Services
{
    internal class FilterObjectService : IFilterObjectService
    {
        private readonly IFrequencyCounterService _frequencyCounterService;
        private const int SKIP_MOST_COMMON_WORDS_COUNT = 5;
        private const int TAKE_MOST_COMMON_WORDS_COUNT = 10;
        public Either<Error, FilterObject> CreateFilterObject(CreateFilterObjectParams createFilterObjectParams)
        {
            var result = Right<FilterObjectContext, FilterObjectContext>(new FilterObjectContext
            {
                Products=createFilterObjectParams.AllProducts
             })
             .Map(GetSizes)
             .Bind(GetMostCommonWords)
             .Map(GetMinMaxPrice)
             .Map(ct=>new FilterObject { 
                 MaxPrice=ct.MaxPrice,
                 MinPrice=ct.MinPrice,
                 MostCommonWords=ct.MostCommonWords,
                 Sizes=ct.Sizes
             })
             .MapLeft(ct=>ct.Error);
            return result;
        }

        private Either<FilterObjectContext, FilterObjectContext> GetMostCommonWords(FilterObjectContext context)
        {
            var descriptions = context.Products.Select(x => x.Description);
            var frequencyMap = _frequencyCounterService.CalculateFrequencyMap(descriptions);
            if (frequencyMap.Count < TAKE_MOST_COMMON_WORDS_COUNT)
            {
                var error = Error.New("Invalid resulting common words count");
                return Left<FilterObjectContext, FilterObjectContext>(context.SetError(error) as FilterObjectContext);
            }
            context.MostCommonWords = frequencyMap
                .Keys
                .Skip(SKIP_MOST_COMMON_WORDS_COUNT)
                .Take(TAKE_MOST_COMMON_WORDS_COUNT).ToList();
            return Right<FilterObjectContext,FilterObjectContext>(context);
        }
        private FilterObjectContext GetMinMaxPrice(FilterObjectContext context)
        {
            context.MinPrice = context.Products.Min(x => x.Price);
            context.MaxPrice = context.Products.Max(x => x.Price);
            return context;
           
        }
        private FilterObjectContext GetSizes(FilterObjectContext context)
        {
            context.Sizes = context.Products.SelectMany(x => x.Sizes).ToHashSet().ToList();
            return context;
        }

        public FilterObjectService(IFrequencyCounterService frequencyCounterService)
        {
                _frequencyCounterService=frequencyCounterService??throw new ArgumentNullException(nameof(frequencyCounterService)); 
        }

    }
}
