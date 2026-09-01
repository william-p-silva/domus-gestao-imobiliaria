

using Domus.Application.DTOs.ApiResponse;
using Domus.Domain.Exceptions.Domain;
using Microsoft.AspNetCore.Diagnostics;

namespace Domus.WebApi.Middlewares;

public sealed class DomusExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DomusExceptionHandler> _logger;

    public DomusExceptionHandler(
        ILogger<DomusExceptionHandler> logger) => _logger = logger;


    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message, title) = GetStatusCodeAndMessage(exception);

        if(statusCode == 500)
        {
            _logger.LogError(exception, 
                "Ocorreu um erro interno no servidor: {Mensagem}", 
                exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, 
                "Exceção tratada. [{StatusCode}] {Mensagem}", 
                statusCode, message);
        }

        var problemDetails = new ErrorResponseApi
        {
            Status = statusCode,
            Title = title,
            Detail = message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private (int, string, string) GetStatusCodeAndMessage(Exception exception)
    {
        return exception switch
        {
            BusinessRuleException =>
                    (400, exception.Message, "Regra de negócio violada."),

            ValidationException =>
                    (409, exception.Message, "Conflito de operação."),

            NotFoundException =>
                    (404, exception.Message, "Recurso não encontrado."),

            DomainException =>
                    (400, exception.Message, "Erro de domínio."),

            InvalidOperationException =>
                    (400, exception.Message, "Operação inválida."),

            UnauthorizedAccessException =>
                    (401, exception.Message, "Acesso não autorizado."),

            ArgumentNullException =>
                    (400, exception.Message, "Argumento nulo."),

            ArgumentException =>
                    (400, exception.Message, "Argumento inválido."),

            _ => 
                (500, "Ocorreu um erro interno no servidor.", "Ocorreu um erro interno no servidor.")
        };
    }
}
