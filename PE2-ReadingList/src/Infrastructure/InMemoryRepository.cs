using ReadingList.Domain;
using ReadingList.Domain.Interfaces;
using ReadingList.Domain.Records;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure;

internal class InMemoryRepository<TKey, T> : IRepository<TKey, T> where T : class
{
    private ConcurrentDictionary<TKey, T> Store { get; } = new();
    private Func<T, TKey> KeySelector { get; }
    
    public InMemoryRepository(Func<T, TKey> keySelector)
    {
        KeySelector = keySelector;
    }
    #region IRepository<T> Members
    public Task<Result> AddAsync(T entity)
    {
        try
        {
            var key = KeySelector(entity);
            if (!Store.TryAdd(key, entity)) return Task.FromResult(Result.Failure(Error.Add));
            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Failure(Error.FromException(ex)));
        }
    }
    public Task<Result> BulkAddAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (AddAsync(entity).Result.ISFailure) continue;
        }
        return Task.FromResult(Result.Success());
    }
    public Task<Result<T>> GetByIdAsync(TKey key)
    {
        try
        {
            if(!Store.TryGetValue(key, out T? entity)) return Task.FromResult(Result.Failure<T>(Error.Get));
            return Task.FromResult(Result.Success(entity));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Failure<T>(Error.FromException(ex)));
        }
    }
    public Task<Result<IReadOnlyList<T>>> ListAsync()
    {
        IReadOnlyList<T> values = [.. Store.Values];
        if (values.Count == 0) return Task.FromResult(Result.Failure<IReadOnlyList<T>>(Error.NullValue));
        return Task.FromResult(Result.Success(values));
    }
    public Task<Result> UpdateAsync(TKey key, T updatedEntity)
    {
        try
        {
            if (!Store.TryGetValue(key, out T? current)) return Task.FromResult(Result.Failure(Error.Get));
            if (!Store.TryUpdate(key, updatedEntity, current)) return Task.FromResult(Result.Failure(Error.Update));
            return Task.FromResult(Result.Success());
        }
        catch(Exception ex) 
        {
            return Task.FromResult(Result.Failure(Error.FromException(ex)));
        }
    }
    public Task<Result> DeleteAsync(TKey key)
    {
        try
        {
            if (!Store.TryRemove(key, out var _)) return Task.FromResult(Result.Failure(Error.Remove));
            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Failure(Error.FromException(ex)));
        }
    }
    #endregion
}
