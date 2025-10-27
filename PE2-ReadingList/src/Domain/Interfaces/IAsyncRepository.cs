namespace ReadingList.Domain.Interfaces;

public interface IRepository<T, TKey> where T : class
{
    Result<List<T>> FetchAll();
    Result<IQueryable<T>> Select();
    Result Add(TKey key, T entity);
    Result Delete(TKey Key);
}
