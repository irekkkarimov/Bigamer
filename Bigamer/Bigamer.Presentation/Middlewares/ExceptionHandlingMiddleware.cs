using System.Text.Json;
using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Presentation.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApplicationBaseException e)
        {
            var newJsonResult = new { statusCode = e.StatusCode, message = e.Message };
            var messageJson = JsonSerializer.Serialize(newJsonResult);
            Console.WriteLine(e.Message);
            context.Response.StatusCode = (int)e.StatusCode;
            context.Response.ContentType = "text/json";
            await context.Response.WriteAsync(messageJson);
        }
        catch (Exception e)
        {
            var newJsonResult = new { statusCode = 500, message = e.Message };
            var messageJson = JsonSerializer.Serialize(newJsonResult);
            Console.WriteLine(e.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json";
            await context.Response.WriteAsync(messageJson);
        }
    }
}