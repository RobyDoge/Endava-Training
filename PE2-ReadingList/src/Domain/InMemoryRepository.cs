using ReadingList.Domain.Interfaces;
using System.Collections.Concurrent;

namespace ReadingList.Domain;

internal class InMemoryRepository<T, TKey> : IAsyncRepository<T> where T : class
{
    private ConcurrentDictionary<TKey, T> Store { get; } = new();

    #region IRepository<T> Members
    public object Add(T entity)
    {
        
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public List<T> FetchAll()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Select()
    {
        throw new NotImplementedException();
    }
    #endregion
}
