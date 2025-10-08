using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message = "Acesso nao autorizado")
            : base(message)
        {
        }
    }
}