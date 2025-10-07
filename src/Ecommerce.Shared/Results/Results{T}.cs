using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Results
{
    public class Results<T> : Result
    {
        public T? Data { get; init; }

        public Results<T> OK(T data, string? message = null)
        {
            return 
        }

        public Results<T> Fail(string error, string? message = null)
        {
            return 
        }
    }


}