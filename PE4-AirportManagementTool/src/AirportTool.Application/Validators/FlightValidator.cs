using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Validators;

public static class FlightValidator
{
    public static bool IsValidFlightNumber(string flightNumber)
    {
        foreach (char c in flightNumber)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    public static bool AreAirportsDifferent(string originIata, string destinationIata)
    {
        return !string.Equals(originIata, destinationIata, StringComparison.OrdinalIgnoreCase);
    }
}