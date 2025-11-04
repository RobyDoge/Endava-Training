namespace ReadingList.Domain.Interfaces;

public interface IRepository<TKey, T> where T : class
{
    Task<Result<IReadOnlyList<T>>> ListAsync();
    Task<Result<T>> GetByIdAsync(TKey key);
    Task<Result> AddAsync(T entity);
    Task<Result> BulkAddAsync(IEnumerable<T> entities);
    Task<Result> DeleteAsync(TKey key);
    Task<Result> UpdateAsync(TKey key, T updatedEntity);
}
