using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrtly.Common
{
    public class Result
    {
        public string Message { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}
