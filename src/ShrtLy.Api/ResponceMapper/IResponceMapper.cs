using Microsoft.AspNetCore.Mvc;
using Shrtly.Common;

namespace ShrtLy.Api.ResponceMapper
{
    public interface IResponseMapper
    {
        IActionResult ExecuteAndMapStatus(Result result);
        IActionResult ExecuteAndMapStatus<T>(Result<PaginatedList<T>> result);
        IActionResult ExecuteAndMapStatus<T>(Result<T> result);
    }

}
