namespace ReadingList.Domain.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<Result<List<T?>> FetchAll();
    TaskIQueryable<T> Select();
    object Add(T entity);
    void Delete(T entity);
    void Save();
}
