using AirportTool.Application.Records;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class UpsertFlightScheduleValidator : AbstractValidator<UpsertFlightScheduleRecord>
{
    public UpsertFlightScheduleValidator()
    {
    }
}