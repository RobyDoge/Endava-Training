using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Abstractions;

public interface IEntityHelper
{
    Task<Result<T>> FindByPropertyAsync<T, V>
      (IQueryable<T> source,
        string propertyName,
        V? value) where T : class;

    Task<Result<T, Error>> FindEntityAsync<T, V>(
           IQueryable<T> source,
           string propertyName,
           V? value) where T : class;

    void Patch<T>(T? newValue, Action<T> apply) where T : class;

    void Patch<T>(T? newValue, Action<T> apply) where T : struct;
}