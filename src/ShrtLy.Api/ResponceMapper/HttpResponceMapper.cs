using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shrtly.Common;
using System.Collections.Generic;

namespace ShrtLy.Api.ResponceMapper
{
    public class HttpResponceMapper : IResponseMapper
    {
        private static readonly Dictionary<ResultStatus, int> StatusMapping = new Dictionary<ResultStatus, int>()
    {
        { ResultStatus.Success,  StatusCodes.Status200OK },
        { ResultStatus.BadRequest, StatusCodes.Status400BadRequest },
        { ResultStatus.Conflict, StatusCodes.Status409Conflict },
        { ResultStatus.NoContent, StatusCodes.Status204NoContent },
        { ResultStatus.NotFound, StatusCodes.Status404NotFound },
        { ResultStatus.Accepted, StatusCodes.Status202Accepted },
        { ResultStatus.PartialContent, StatusCodes.Status206PartialContent },
        { ResultStatus.Forbidden, StatusCodes.Status403Forbidden },
        { ResultStatus.Created, StatusCodes.Status201Created },
        { ResultStatus.TooManyRequests, StatusCodes.Status429TooManyRequests },
    };

        public IActionResult ExecuteAndMapStatus(Result result)
        {
            int statusCode = GetStatusCode(result.ResultStatus);
            var actionResult = new ObjectResult(result.Message)
            {
                StatusCode = statusCode
            };

            return actionResult;
        }

        public IActionResult ExecuteAndMapStatus<T>(Result<PaginatedList<T>> result)
        {
            var statusCode = GetStatusCode(result.ResultStatus);
            var actionResult = (result.Data == null) ? new ObjectResult(result.Message)
                : new ObjectResult(new
                {
                    result.Data,
                    result.Data.TotalRecords,
                    result.Data.PageIndex,
                    result.Data.PageSize,
                    result.Data.TotalPages,
                    result.Data.HasPreviousPage,
                    result.Data.HasNextPage
                });

            actionResult.StatusCode = statusCode;

            return actionResult;
        }

        public IActionResult ExecuteAndMapStatus<T>(Result<T> result)
        {
            var statusCode = GetStatusCode(result.ResultStatus);
            var actionResult = (result.Data == null) ? new ObjectResult(result.Message) : new ObjectResult(result.Data);

            actionResult.StatusCode = statusCode;

            return actionResult;
        }

        private static int GetStatusCode(ResultStatus statusCode)
        {
            return StatusMapping.ContainsKey(statusCode) ? StatusMapping[statusCode] : StatusCodes.Status500InternalServerError;
        }
    }
}
