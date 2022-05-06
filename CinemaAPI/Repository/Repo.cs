using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.API.Data;
using CinemaLib;

namespace ProgettoCinema.API.Repository;

public class GenericRepository<TEntity> where TEntity : Base
{

    internal CinemaDbContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(CinemaDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task<TEntity> GetById(int id,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        await Delete(entityToDelete);
    }

    public virtual async Task Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
        await context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}

