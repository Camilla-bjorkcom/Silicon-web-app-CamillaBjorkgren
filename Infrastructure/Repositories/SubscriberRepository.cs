using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SubscriberRepository(WebApiDbContext context) : Repo<SubscriberEntity, WebApiDbContext>(context)
{
    private readonly WebApiDbContext _context = context;
}

