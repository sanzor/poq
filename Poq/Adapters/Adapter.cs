using Poq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poq
{
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
