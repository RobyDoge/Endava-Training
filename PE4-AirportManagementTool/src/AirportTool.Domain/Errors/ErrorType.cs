using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Domain.Errors;

public enum ErrorType
{
    NotFound,
    Validation,
    Exception
}