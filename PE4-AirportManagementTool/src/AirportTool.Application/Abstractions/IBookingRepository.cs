using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IBookingRepository
{
    Task<Result<BookingEntity, Error>> GetAsync(int id);

    Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateBookingRecord record);

    Task<Result<BookingEntity, Error>> GetByConfirmationCodeAsync(string confirmationCode);

    Task<UnitResult<Error>> CancelAsync(string confimationCode);
}