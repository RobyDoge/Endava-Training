using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(int? id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task Delete(int id);
    Task<bool> Exists(int id);
}
