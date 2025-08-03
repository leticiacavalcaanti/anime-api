using AnimeApp.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace AnimeApp.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AnimeNotFoundException ex)
        {
            logger.LogWarning(ex, "Anime não encontrado");
            await WriteErrorResponse(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro interno não tratado");
            await WriteErrorResponse(context, HttpStatusCode.InternalServerError, "Erro interno no servidor.");
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new { error = message };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
