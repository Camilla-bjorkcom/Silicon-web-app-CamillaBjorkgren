using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ContactMessageRepository(WebApiDbContext context) : Repo<ContactMessageEntity, WebApiDbContext>(context)
{
    private readonly WebApiDbContext _context = context;
}
