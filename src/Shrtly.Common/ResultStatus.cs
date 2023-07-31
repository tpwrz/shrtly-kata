using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrtly.Common
{
    public enum ResultStatus
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        PartialContent = 206,
        BadRequest = 400,
        Conflict = 409,
        NotFound = 404,
        Forbidden = 403,
        TooManyRequests = 429,
        InternalServerError = 500
    }
}
