using AirportTool.Application.Abstractions;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Utils;

public class EntityHelper : IEntityHelper
{
    public async Task<Result<T>> FindByProperty<T, V>
       (IQueryable<T> source, string propertyName, V? value) where T : class
    {
        if (value is null)
            return Result.Success<T>(null!);
        if (string.IsNullOrWhiteSpace(propertyName))
            return Result.Failure<T>("Property name cannot be null or whitespace.");

        if (value is string && string.IsNullOrWhiteSpace(value as string))
            return Result.Success<T>(null!);

        var result = await source.SingleOrDefaultAsync(a => EF.Property<V>(a, propertyName)!.Equals(value));

        return result == null
        ? Result.Failure<T>($"{typeof(T).Name} with {propertyName} '{value}' not found.")
        : Result.Success(result);
    }

    public async Task<Result<T, Error>> FindRequiredEntity<T, V>(
            IQueryable<T> source,
            string propertyName,
            V? value)
    where T : class
    {
        var res = await FindByProperty(source, propertyName, value);

        if (res.IsFailure)
            return Result.Failure<T, Error>(Error.NotFound(typeof(T).Name, value!.ToString()));

        return Result.Success<T, Error>(res.Value!);
    }

    public void Patch<T>(T? newValue, Action<T> apply) where T : class
    {
        if (newValue is not null) apply(newValue);
    }

    public void Patch<T>(T? newValue, Action<T> apply) where T : struct
    {
        if (newValue.HasValue) apply(newValue.Value);
    }
}