using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Services;

public class BookingService
{
    public IServiceProvider ServiceProvider { get; }
    public IUnitOfWork UnitOfWork { get; }

    public BookingService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
    {
        ServiceProvider = serviceProvider;
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<BookingEntity, Error>> CreateAsync(CreateBookingRecord record)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateBookingRecord>>()
            ?? throw new InvalidOperationException("CreateBookingValidator not registered in the service provider.");

        var validationResult = validator.Validate(record);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("; ", validationResult.Errors);
            return Result.Failure<BookingEntity, Error>(Error.Validation(errorMessage));
        }
        record = record with
        {
            ConfirmationCode = CreateConfirmatioCode(),
            CreatedAt = DateTime.UtcNow
        };

        var resultedId = await UnitOfWork.BookingRepository.CreateAsync(record);

        await UnitOfWork.SaveChangesAsync();
        if (resultedId.IsFailure) return Result.Failure<BookingEntity, Error>(resultedId.Error);

        return await UnitOfWork.BookingRepository.GetAsync(resultedId.Value.Id);
    }

    public Task<Result<BookingEntity, Error>> GetByConfirmationCodeAsync(string confirmationCode)
    {
        if (string.IsNullOrEmpty(confirmationCode) || confirmationCode.Length > 8)
        {
            return Task.FromResult(Result.Failure<BookingEntity, Error>(Error.Validation("Invalid confirmation code.")));
        }

        return UnitOfWork.BookingRepository.GetByConfirmationCodeAsync(confirmationCode);
    }

    public Task<UnitResult<Error>> CancelAsync(string confimationCode)
    {
        throw new NotImplementedException();
    }

    private static string CreateConfirmatioCode()
    {
        var guid = Guid.NewGuid().ToString("N").ToUpper();
        return guid.Substring(0, 8);
    }
}