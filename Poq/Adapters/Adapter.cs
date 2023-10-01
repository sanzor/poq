using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq
{
    /// <summary>
    /// This class is a adapter layer , so that in the even that the input of the request differs, 
    /// our domain model remains the same , and we only make changes here
    /// </summary>
    public class Adapter


    {
        public static Either<Error,GetProductsParams> Adapt(GetProductsFilterParamsDto dto)
        {
            var rez=Try(() =>
            {
                return new GetProductsParams
                {
                  Highlight =dto.Highlight,
                  MinPrice=dto.MinPrice,
                  MaxPrice=dto.MaxPrice

                };
            }).ToEither(exc => Error.New(exc));
            return rez;
        }
     
    }
}
