
namespace Poq.Services;

public interface IFilterObjectService
{
    Either<Error, FilterObject> CreateFilterObject(CreateFilterObjectParams createFilterObjectParams);
}
