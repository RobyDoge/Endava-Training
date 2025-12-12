namespace AirportTool.Application.Abstractions;

public interface IEntity<T>
{
    T Id { get; }
}