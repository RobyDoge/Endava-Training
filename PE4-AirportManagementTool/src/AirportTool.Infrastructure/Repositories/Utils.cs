using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Repositories;

public static class Utils
{
    public static async Task<Result<T>> FindByPropertyString<T>
        (IQueryable<T> source, string propertyName, string? value) where T : class
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Success<T>(null!);
        if (string.IsNullOrWhiteSpace(propertyName))
            return Result.Failure<T>("Property name cannot be null or whitespace.");

        var result = await source.SingleOrDefaultAsync(a => EF.Property<string>(a, propertyName) == value);

        return result == null
            ? Result.Failure<T>($"{typeof(T).Name} with {propertyName} '{value}' not found.")
            : Result.Success(result);
    }

    public static void Patch<T>(T? newValue, Action<T> apply) where T : class
    {
        if (newValue is not null) apply(newValue);
    }

    public static void Patch<T>(T? newValue, Action<T> apply) where T : struct
    {
        if (newValue.HasValue) apply(newValue.Value);
    }
}