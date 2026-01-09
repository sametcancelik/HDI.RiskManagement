// WebAPI/Middlewares/ExceptionMiddleware.cs
using HDI.Application.Common;
using HDI.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace HDI.WebAPI.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Bir hata oluştu: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new ApiResponse 
        {
            IsSuccess = false,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Sunucu tarafında beklenmedik bir hata oluştu."
        };

        if (exception is BusinessException businessEx)
        {
            response.StatusCode = businessEx.StatusCode;
            response.Message = businessEx.Message;
        }
        else
        {
            response.Errors = new List<string> { exception.Message };
        }

        context.Response.StatusCode = response.StatusCode;
        
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}