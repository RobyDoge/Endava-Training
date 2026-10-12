using AirportTool.Application.Records;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class UpdateTicketValidator : AbstractValidator<UpdateTicketRecord>
{
    public UpdateTicketValidator()
    {
        RuleFor(x => x.FlightScheduleId)
            .GreaterThan(0).When(x => x.FlightScheduleId.HasValue)
            .WithMessage("FlightScheduleId must be greater than 0.");

        RuleFor(x => x.BasePrice)
            .GreaterThanOrEqualTo(0).When(x => x.BasePrice.HasValue)
            .WithMessage("BasePrice must be greater than or equal to 0.");

        RuleFor(x => x.Taxes)
            .GreaterThanOrEqualTo(0).When(x => x.Taxes.HasValue)
            .WithMessage("Taxes must be greater than or equal to 0.");

        RuleFor(x => x.SeatInventory)
            .GreaterThanOrEqualTo(0).When(x => x.SeatInventory.HasValue)
            .WithMessage("SeatInventory must be greater than or equal to 0.");
    }
}