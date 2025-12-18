using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Services.Middlewares;

public class ExceptionMiddleware
{
    public RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var response = new { message = "An unexpected error occurred.", details = ex.Message };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}