using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreStarter.Infrastructure;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public static class MapperExtensions
{
    public static async Task<List<T>> ProjectToListAsync<T>(this IQueryable queryable)
    {
        return await queryable.ProjectTo<T>().ToListAsync();
    }

    public static IQueryable<T> ProjectToQueryable<T>(this IQueryable queryable)
    {
        return queryable.ProjectTo<T>();
    }

    public static async Task<PaginatedList<T>> ProjectToPaginatedList<T>(this IQueryable queryable, int pageNumber, int pageSize)
    {
        return new PaginatedList<T>(await queryable.ProjectTo<T>().ToListAsync(), pageNumber, pageSize);
    }

    public static async Task<T> ProjectToSingleOrDefaultAsync<T>(this IQueryable queryable)
    {
        return await queryable.ProjectTo<T>().SingleOrDefaultAsync();
    }

    public static async Task<T> ProjectToSingleOrDefaultAsync<T>(this IQueryable source, Func<T, bool> predicate)
    {
        return await source.ProjectTo<T>().SingleOrDefaultAsync(t => predicate.Invoke(t));
    }

    public static async Task<T> ProjectToFirstOrDefaultAsync<T>(this IQueryable queryable)
    {
        return await queryable.ProjectTo<T>().FirstOrDefaultAsync();
    }

    public static async Task<T> ProjectToFirstOrDefaultAsync<T>(this IQueryable source, Func<T, bool> predicate)
    {
        return await source.ProjectTo<T>().FirstOrDefaultAsync(t => predicate.Invoke(t));
    }

    public static async Task<T> ProjectToLastOrDefaultAsync<T>(this IQueryable queryable)
    {
        return await queryable.ProjectTo<T>().LastOrDefaultAsync();
    }

    public static async Task<T> ProjectToLastOrDefaultAsync<T>(this IQueryable source, Func<T, bool> predicate)
    {
        return await source.ProjectTo<T>().LastOrDefaultAsync(t => predicate.Invoke(t));
    }
}
