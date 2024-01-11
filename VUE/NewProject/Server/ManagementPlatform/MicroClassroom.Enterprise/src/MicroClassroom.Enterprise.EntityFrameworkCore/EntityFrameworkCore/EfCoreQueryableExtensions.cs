using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public static class EfCoreQueryableExtensions
{
    public static IQueryable<Mechanism> IncludeDetails(this IQueryable<Mechanism> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }
        return queryable
            .Include(x => x.Banners)
            .Include(x => x.Teachers)
            .Include(x => x.Courses);
    }
}
