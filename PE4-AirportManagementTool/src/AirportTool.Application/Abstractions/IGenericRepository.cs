using AirportTool.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(int? id);

    Task<PagedResult<TResult>> GetAllAsync<TResult>(QuerryParameters querryParameters);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task<bool> DeleteAsync(int id);

    Task<bool> Exists(int id);
}