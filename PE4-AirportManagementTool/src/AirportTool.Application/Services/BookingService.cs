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

    public async Task<Result<BookingEntity, Error>> GetByConfirmationCodeAsync(string confirmationCode)
    {
        if (!ValidateConfirmationCode(confirmationCode))
        {
            return Result.Failure<BookingEntity, Error>(Error.Validation("Invalid confirmation code."));
        }

        return await UnitOfWork.BookingRepository.GetByConfirmationCodeAsync(confirmationCode);
    }

    public async Task<UnitResult<Error>> CancelAsync(string confirmationCode)
    {
        if (!ValidateConfirmationCode(confirmationCode))
        {
            return UnitResult.Failure(Error.Validation("Invalid confirmation code."));
        }

        var getResult = await UnitOfWork.BookingRepository.CancelAsync(confirmationCode);
        await UnitOfWork.SaveChangesAsync();

        return getResult;
    }

    private static string CreateConfirmatioCode()
    {
        var guid = Guid.NewGuid().ToString("N").ToUpper();
        return guid.Substring(0, 8);
    }

    private static bool ValidateConfirmationCode(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length != 8)
        {
            return false;
        }
        return true;
    }
}