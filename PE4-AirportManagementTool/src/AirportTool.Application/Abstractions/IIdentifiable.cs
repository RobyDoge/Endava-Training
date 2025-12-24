namespace AirportTool.Application.Abstractions;

public interface IIdentifiable<T>
{
    T Id { get; }
}