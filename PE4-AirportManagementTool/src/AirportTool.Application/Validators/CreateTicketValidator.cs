using AirportTool.Application.Records;
using AirportTool.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class CreateTicketValidator : AbstractValidator<CreateTicketRecord>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.FlightScheduleId)
            .GreaterThan(0).WithMessage("Flight Schedule Id must be greater than 0");

        RuleFor(x => x.BasePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Base Price must be greater than or equal to 0");

        RuleFor(x => x.Taxes)
            .GreaterThanOrEqualTo(0).WithMessage("Taxes must be greater than or equal to 0");

        RuleFor(x => x.SeatInventory)
            .GreaterThanOrEqualTo(0).WithMessage("Seat Inventory must be greater than 0");
    }
}