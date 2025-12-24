using AirportTool.Application.Records;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingRecord>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty()
            .WithMessage("TicketId is required.")
            .GreaterThan(0)
            .WithMessage("TicketId must be greater than 0.");

        RuleFor(x => x.PassengerFullName)
            .NotEmpty()
            .WithMessage("PassengerFullName is required.")
            .MaximumLength(120)
            .WithMessage("PassengerFullName must not exceed 120 characters.");

        RuleFor(x => x.PassengerEmail)
            .NotEmpty()
            .WithMessage("PassengerEmail is required.")
            .EmailAddress()
            .WithMessage("PassengerEmail must be a valid email address.")
            .MaximumLength(120)
            .WithMessage("PassengerEmail must not exceed 120 characters.");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required.")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");
    }
}