using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zuhid.BaseApi;

public class ActionFilter(ILogger<ActionFilter> logger) : IActionFilter {

  public void OnActionExecuting(ActionExecutingContext context) {
    var controllerAction = $"{context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}";
    var action = $"{context.ActionDescriptor.RouteValues["action"]}";
    var queryString = context.HttpContext.Request.QueryString.Value;

    // // require the action items to be not null
    // foreach (var kvp in context.ActionArguments)
    // {
    //     if (kvp.Value == null) throw new ArgumentNullException(kvp.Key);
    // }

    logger.LogInformation(new EventId(0, controllerAction), JsonSerializer.Serialize(new {
      context.HttpContext.User?.Identity?.Name,
      action,
      queryString,
      context.HttpContext.Request.Path,
      context.ActionArguments
    }, JsonSerializerOption));
  }

  public void OnActionExecuted(ActionExecutedContext context) {
    var objectResult = context.Result as ObjectResult;
    var controllerAction = $"{context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}";
    var action = $"{context.ActionDescriptor.RouteValues["action"]}";
    var queryString = context.HttpContext.Request.QueryString.Value;

    if (context.Exception != null) {
      var id = Guid.NewGuid();
      logger.LogError(new EventId(0, controllerAction), JsonSerializer.Serialize(new {
        id,
        controllerAction,
        action,
        queryString,
        context.HttpContext.Request.Path,
        // body = ReadRequestBody(context.HttpContext),
        Exception = context.Exception.Message,
        context.Exception.StackTrace,
      }, JsonSerializerOption));
      context.Result = new ObjectResult($"Internal Server Error with Id = {id}") {
        StatusCode = StatusCodes.Status500InternalServerError
      };
      context.ExceptionHandled = true;
    }

    // If ModelState is invalid then return a badrequest
    else if (!context.ModelState.IsValid) {
      logger.LogWarning(new EventId(0, controllerAction), JsonSerializer.Serialize(new { context.ModelState }, JsonSerializerOption));
      context.Result = new BadRequestObjectResult(context.ModelState);
    } else {
      logger.LogInformation(new EventId(0, controllerAction), JsonSerializer.Serialize(new {
        context.HttpContext.User?.Identity?.Name,
        objectResult = objectResult?.Value,
      }, JsonSerializerOption));
    }
  }

  // private static string ReadRequestBody(HttpContext context)
  // {
  //     using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
  //     var body = reader.ReadToEndAsync().Result;
  //     // context.Request.Body.Position = 0;
  //     return body;
  //     // context.Request.EnableBuffering();
  //     // using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
  //     // var body = reader.ReadToEndAsync().GetAwaiter().GetResult();
  //     // context.Request.Body.Position = 0; // Reset the stream position to allow further processing
  //     // return body;
  // }

  private static readonly JsonSerializerOptions JsonSerializerOption = new() {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };
}
