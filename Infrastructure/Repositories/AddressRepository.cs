using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepository(DataContext context) : Repo<AddressEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<AddressEntity>> GetAllAsync()
    {
        try
        {
            IEnumerable<AddressEntity> result = await _context.Set<AddressEntity>()
                .Include(i => i.User)
                .ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            return null!;
        }
    }

    public override async Task<AddressEntity> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<AddressEntity>()
                .Include(i => i.User)
                .FirstOrDefaultAsync(predicate);


            return result!;
        }
        catch (Exception ex)
        {
            return null!;
        }
    }
}

