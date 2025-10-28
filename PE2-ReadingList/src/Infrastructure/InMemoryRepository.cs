using ReadingList.Domain;
using ReadingList.Domain.Interfaces;
using ReadingList.Domain.Records;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure;

internal class InMemoryRepository<TKey, T> : IRepository<TKey, T> where T : class
{
    private ConcurrentDictionary<TKey, T> Store { get; } = new();

    #region IRepository<T> Members
    public Result Add(TKey key, T entity)
    {
        try
        {
            if (!Store.TryAdd(key, entity)) return Result.Failure(Error.Add);
            return Result.Success();
        }
        catch (Exception ex) 
        {
            return Result.Failure(Error.FromException(ex));
        }
    }
    public Result Delete(TKey key)
    {
        try
        {
            if (!Store.TryRemove(key, out var _)) return Result.Failure(Error.Remove);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.FromException(ex));
        }
    }
    public Result<List<T>> FetchAll()
    {
        List<T> values = [.. Store.Values];
        if (values.Count == 0) return Result.Failure<List<T>>(Error.NullValue);
        return Result.Success(values);
    }
    public Result<IQueryable<T>> Select()
    {
        try { return Result.Success(Store.Values.AsQueryable()); }
        catch (Exception ex) { return Result.Failure<IQueryable<T>>(Error.FromException(ex)); }
    }
    #endregion
}
