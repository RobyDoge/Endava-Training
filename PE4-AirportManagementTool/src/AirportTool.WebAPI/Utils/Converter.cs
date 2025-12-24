using AirportTool.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Utils;

public class Converter
{
    public static IActionResult ErrorToActionResult(Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(error),
            ErrorType.Validation => new BadRequestObjectResult(error),
            _ => new BadRequestObjectResult(error),
        };
    }
}