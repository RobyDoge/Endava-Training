using AirportTool.Application.Abstractions;
using AirportTool.Application.Models;
using AirportTool.Infrastructure.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private AirlineBookingContext Context { get; }
    public IMapper Mapper { get; }

    public GenericRepository(AirlineBookingContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity is null) return false;
        Context.Set<T>().Remove(entity);
        return true;
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
        Context.Set<T>().Update(entity);
    }

    public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QuerryParameters querryParameters)
    {
        var totalSize = await Context.Set<T>().CountAsync();
        var items = await Context.Set<T>()
            .Skip(querryParameters.PageNumber)
            .Take(querryParameters.PageSize)
            .ProjectTo<TResult>(Mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<TResult>
        {
            Items = items,
            PageNumber = querryParameters.PageNumber,
            RecordNumber = querryParameters.PageSize,
            TotalCount = totalSize
        };
    }
}