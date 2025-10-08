using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Shared.Exceptions;
using Ecommerce.Shared.Results;

namespace Ecommerce.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static Result ToResult(this Exception ex)
        {
            return ex switch
            {
                ValidationException validationEX => Result.Fail(
                    "Falha de validação",
                    string.Join("; ", validationEX.Errors.SelectMany(
                        e => e.Value.Select(msg => $"{e.Key}: {msg}")
                    ))
                ),

                NotFoundException => Result.Fail("Recurso nao encontrado"),
                UnauthorizedException => Result.Fail("Acesso não autorizado"),
                DomainException => Result.Fail("Erro de dominio", ex.Message),

                _ => Result.Fail("Erro inesperado", ex.Message)
            };
        }

        public static Results<T> ToResult<T>(this Exception ex)
        {
            return ex switch
            {
                ValidationException validationEX => Results<T>.Fail(
                    "Falha de Validação",
                    string.Join("; ", validationEX.Errors.SelectMany(
                        e => e.Value.Select(msg => $"{e.Key}: {msg}")
                    ))
                ),

                NotFoundException => Results<T>.Fail("Recurso não encontrado"),
                UnauthorizedException => Results<T>.Fail("Acesso não autorizado"),
                DomainException => Results<T>.Fail("Erro de dominio", ex.Message),
                _ => Results<T>.Fail("Erro inesperado", ex.Message)
            };
        }
    }
}