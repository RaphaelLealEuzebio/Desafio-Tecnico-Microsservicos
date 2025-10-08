using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Results
{
    public class Results<T> : Result
    {
        public T? Data { get; init; }

        public static Results<T> OK(T data, string? message = null)
        {
            return new Results<T>
            {
                Success = true,
                Data = data,
                Message = message ?? string.Empty
            };
        }

        public new static Results<T> Fail(string error, string? message = null)
        {
            return new Results<T>
            {
                Success = false,
                Error = error,
                Message = message ?? string.Empty
            };
        }
    }


}