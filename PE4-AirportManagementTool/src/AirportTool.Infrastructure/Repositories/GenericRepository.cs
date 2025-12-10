using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private AirlineBookingContext Context { get; }
    public GenericRepository(AirlineBookingContext context) => Context = context;

    public async Task<T> AddAsync(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(int id)
    {
        var entity = await GetAsync(id);
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int? id)
    {
        if (id is null) return null;

        return await Context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }
}
