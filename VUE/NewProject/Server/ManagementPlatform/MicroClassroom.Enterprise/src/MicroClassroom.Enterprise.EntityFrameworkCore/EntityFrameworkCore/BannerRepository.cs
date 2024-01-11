using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class BannerRepository : EfCoreRepository<EnterpriseDbContext, Banner, Guid>, IBannerRepository
{
    public BannerRepository(IDbContextProvider<EnterpriseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
