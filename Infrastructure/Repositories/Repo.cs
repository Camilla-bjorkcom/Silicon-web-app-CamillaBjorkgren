using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Infrastructure.Repositories;

public abstract class Repo<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;

    protected Repo(TContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
           await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;

    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            var entityToUpdate = await _context.Set<TEntity>().FindAsync(GetKeyValues(entity));
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entityToUpdate;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return false;
    }

    public virtual async Task<bool> DeleteAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();

            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Remove(entity);
            }

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
            return false;
        }
    }

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var exisiting = await _context.Set<TEntity>().AnyAsync(predicate);

            return exisiting;
        }
        catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

        return false;
    }

    private object[] GetKeyValues(TEntity entity)
    {
        var keyProperties = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties;
        var keyValues = new object[keyProperties.Count];
        for (var i = 0; i < keyProperties.Count; i++)
        {
            keyValues[i] = entity.GetType().GetProperty(keyProperties[i].Name)?.GetValue(entity);
        }
        return keyValues;
    }
}
