using Domus.Application.DTOs.ApiResponse;

namespace Domus.WebApi.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ArgumentException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new ErroApiResponseDTO
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Success = false,
                    Message = ex.Message
                };
                
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new ErroApiResponseDTO
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Success = false,
                    Message = ex.Message
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new ErroApiResponseDTO
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Success = false,
                    Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde." + ex.Message
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
