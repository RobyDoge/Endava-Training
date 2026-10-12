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
        RuleFor(fs => fs.Id)
            .GreaterThan(0)
            .When(fs => fs.Id.HasValue)
            .WithMessage("Id must be greater than 0");

        RuleFor(x => x.AssignedAircraftTailName)
            .MaximumLength(10)
            .When(x => x.AssignedAircraftTailName is not null)
            .WithMessage("Aircraft's tail name must be 10 chars long");

        RuleFor(x => x)
            .Must(x =>
                !x.ScheduledArrivalUtc.HasValue ||
                x.ScheduledDepartureUtc < x.ScheduledArrivalUtc.Value)
            .WithMessage("Departure date must be before arrival time");
    }
}