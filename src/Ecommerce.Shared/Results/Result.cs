using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Results
{
    //Resultado das Operações
    public class Result
    {
        //Diz se a operação deu certo
        public bool Success { get; init; }
        //Mensagem, Exemplo: Produto Criado com Sucesso
        public string? Message { get; init; }
        //Mensagem Tecnica: Falha ao se conectar com o banco
        public string? Error { get; init; }

        //Modelo para Sucesso
        public static Result OK(string? message = null)
        {
            return new Result
            {
                Success = true,
                Message = message
            };

        }
        //Modelo para Falha
        public static Result Fail(string error, string? message = null)
        {
            return new Result
            {
                Success = false,
                Error = error,
                Message = message
            };
        }
        // 💡 Curiosidade:
        // Esse padrão (Result.Ok() e Result.Fail()) é inspirado em uma ideia 
        // muito usada em DDD (Domain-Driven Design) e Functional Programming.
        // É uma forma mais segura e sem exceções de representar erros.
    }
}