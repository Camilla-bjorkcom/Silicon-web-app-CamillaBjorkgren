using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext context) : Repo<UserEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<UserEntity> result = await _context.Set<UserEntity>()
                .Include(i => i.Address)
                .ToListAsync();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<UserEntity>()
                .Include(i => i.Address)
                .FirstOrDefaultAsync(predicate);

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
}

