using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zuhid.BaseApi;

public class ExceptionFilter(ILogger<ActionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Log the exception
        // logger.LogError(context.Exception, context.ActionDescriptor);

        var objectResult = context.Result as ObjectResult;
        var controllerAction = $"{context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}";
        var action = $"{context.ActionDescriptor.RouteValues["action"]}";
        var queryString = context.HttpContext.Request.QueryString.Value;
        logger.LogError(context.Exception, JsonSerializer.Serialize(new
        {
            // id,
            controllerAction,
            action,
            queryString,
            context.HttpContext.Request.Path,
            body = ReadRequestBody(context.HttpContext),
            Exception = context.Exception.Message,
            context.Exception.StackTrace,
        }, jsonSerializerOptions));

        // Customize the response
        context.Result = new ObjectResult(new
        {
            Error = "An unexpected error occurred.",
            Details = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        // Mark the exception as handled
        context.ExceptionHandled = true;
    }

    private static string ReadRequestBody(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var body = reader.ReadToEndAsync().Result;
        // context.Request.Body.Position = 0;
        return body;
    }

    private static JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}
