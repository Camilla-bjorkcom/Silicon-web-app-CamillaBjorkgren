using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class Repo<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected Repo(DataContext context)
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
                var entityToUpdate = await _context.Set<TEntity>().FindAsync(entity);
                if (entityToUpdate != null)
                {
                    entityToUpdate = entity;
                    _context.Set<TEntity>().Update(entityToUpdate);
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
    }
}
