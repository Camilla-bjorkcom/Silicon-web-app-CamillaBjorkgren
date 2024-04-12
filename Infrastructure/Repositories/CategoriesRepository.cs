using Infrastructure.Contexts;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class CategoriesRepository(WebApiDbContext context) : Repo<CategoryEntity, WebApiDbContext>(context)
{
    private readonly WebApiDbContext _context = context;
}

