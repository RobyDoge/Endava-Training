using AirportTool.Application.Records;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public class UpdateFlightValidator : AbstractValidator<UpdateFlightRecord>
{
    public UpdateFlightValidator()
    {
        RuleFor(x => x.AirlineIata)
            .Length(2).When(x => x.AirlineIata is not null)
            .WithMessage("Airline Iata must be 2 characters long");

        RuleFor(x => x.FlightNumber)
            .Cascade(CascadeMode.Stop)
            .Length(1, 10).WithMessage("Flight number must contain between 1 and 10 characters")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Flight number must contain only letters and numbers")
            .When(x => x.FlightNumber is not null);

        RuleFor(x => x.OriginAirportIata)
            .Length(3).When(x => x.OriginAirportIata is not null)
            .WithMessage("Origin Airport Iata must be 3 characters long");

        RuleFor(x => x.DestinationAirportIata)
            .Length(3).When(x => x.DestinationAirportIata is not null)
            .WithMessage("Destination Airport Iata must be 3 characters long");

        RuleFor(x => x)
            .Must(x => x.OriginAirportIata != x.DestinationAirportIata)
            .When(x => x.OriginAirportIata is not null && x.DestinationAirportIata is not null)
            .WithMessage("Origin and Destination airports must be different");
    }
}