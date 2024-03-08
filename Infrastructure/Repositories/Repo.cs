using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public virtual async Task<ResponseResult> CreateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok(entity);
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<ResponseResult> GetAllAsync()
        {
            try
            {
                IEnumerable<TEntity> result = await _context.Set<TEntity>().ToListAsync();
                return ResponseFactory.Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (result == null)
                {
                    return ResponseFactory.NotFound();
                }
                return ResponseFactory.Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<ResponseResult> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(updatedEntity);
                    await _context.SaveChangesAsync();
                    return ResponseFactory.Ok(result);
                }
                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<ResponseResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
                if (result != null)
                {
                    _context.Set<TEntity>().Remove(result);
                    await _context.SaveChangesAsync();
                    return ResponseFactory.Ok("Successfully Removed");
                }
                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<ResponseResult> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<TEntity>().AnyAsync(predicate);
                if (result)
                {
                    return ResponseFactory.Exists();
                }
                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
