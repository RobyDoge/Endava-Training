using AirportTool.Application.Records;
using FluentValidation;

namespace AirportTool.Application.Validators;

public class CreateFlightScheduleValidator : AbstractValidator<CreateFlightScheduleRecord>
{
    public CreateFlightScheduleValidator()
    {
        RuleFor(fs => fs.FlightId)
            .GreaterThan(0).WithMessage("FlightId must be greater than 0.");

        RuleFor(fs => fs.ScheduledDepartureUtc)
            .LessThan(fs => fs.ScheduledArrivalUtc).WithMessage("ScheduledDepartureUtc must be earlier than ScheduledArrivalUtc.");

        RuleFor(fs => fs.GateCode)
            .NotEmpty().WithMessage("GateCode is required.")
            .MaximumLength(10).WithMessage("GateCode must not exceed 10 characters.");

        RuleFor(fs => fs.AssignedAircraftTailName)
            .MaximumLength(10).WithMessage("AssignedAircraftTailName must not exceed 10 characters.")
            .When(fs => !string.IsNullOrEmpty(fs.AssignedAircraftTailName));
    }
}