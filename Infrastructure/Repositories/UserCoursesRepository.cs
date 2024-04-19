using Infrastructure.Contexts;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UserCoursesRepository(WebAppDbContext context) : Repo<UserCoursesEntity, WebAppDbContext>(context)
{
    private readonly WebAppDbContext _context = context;


}
