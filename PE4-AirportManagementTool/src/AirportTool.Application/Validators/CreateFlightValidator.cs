using AirportTool.Application.Records;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class CreateFlightValidator : AbstractValidator<CreateFlightRecord>
{
    public CreateFlightValidator()
    {
        RuleFor(f => f.AirlineIata)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(2, 2)
            .WithErrorCode("AirlineIata must be 2 characters long");

        RuleFor(f => f.FlightNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^[a-zA-Z0-9]+$")
            .WithErrorCode("Flight number cannot contain more than 10 characters and it must contain only letters and numbers");

        RuleFor(f => f.OriginAirportIata)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(3, 3)
            .WithErrorCode("Airport iata must be 3 characters long ");

        RuleFor(f => f.DestinationAirportIata)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(3, 3)
            .WithMessage("Airpot uata must be 3 characters long.")
            .NotEqual(f => f.OriginAirportIata)
            .WithMessage("Origin and Destination airports must be different");
    }
}