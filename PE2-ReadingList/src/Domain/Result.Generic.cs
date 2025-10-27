﻿using ReadingList.Domain.Records;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ReadingList.Domain;

public class Result<T> : Result
{
    private readonly T _value;
    [NotNull]
    public T Value => _value ?? throw new InvalidOperationException("Result has no value.");
    protected internal Result(T? value, bool isSuccess, Error error): base(isSuccess,error)
        => _value = value;
    public static implicit operator Result<T>(T? value) => Create(value);
}
