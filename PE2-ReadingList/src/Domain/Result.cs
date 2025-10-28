﻿using ReadingList.Domain.Records;
using System.Net.Http.Headers;

namespace ReadingList.Domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch(isSuccess) 
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;

        }
    }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default,false, error);
    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success<T>(value) : Failure<T>(Error.NullValue); 

}
