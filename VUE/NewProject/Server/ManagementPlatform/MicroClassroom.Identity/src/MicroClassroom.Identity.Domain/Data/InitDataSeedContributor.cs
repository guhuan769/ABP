using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

namespace MicroClassroom.Identity.Data;

public class InitDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IImporter _importer;
    private readonly IdentityUserManager _userManager;
    private readonly IVirtualFileProvider _virtualFileProvider;

    public InitDataSeedContributor(ITenantRepository tenantRepository,
        IImporter importer,
        IdentityUserManager userManager,
         IVirtualFileProvider virtualFileProvider)
    {
        _tenantRepository = tenantRepository;
        _importer = importer;
        _userManager = userManager;
        _virtualFileProvider = virtualFileProvider;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        var tenantId = context.TenantId;
        if (tenantId.HasValue)
        {
            Tenant tenant = await _tenantRepository.GetAsync(tenantId.Value);

            var userTupleList = GetImportUser(tenant);
            foreach (var tuple in userTupleList)
            {
                await _userManager.CreateAsync(tuple.Item1, tuple.Item2, false);
            }
        }
    }

    private IEnumerable<Tuple<IdentityUser, string>> GetImportUser(Tenant tenant)
    {
        // 文件路径需要加上默认命名空间
        var fileInfo = _virtualFileProvider.GetFileInfo($"/MicroClassroom/Identity/DbMigrator/Files/{tenant.Name}/user.xlsx");
        var import = _importer.Import<ImportUserDto>(fileInfo.CreateReadStream());

        var userList = new List<Tuple<IdentityUser, string>>();
        if (import.IsCompleted && import.Result != null)
        {
            var importUserDtos = import.Result.Data;
            foreach (var dto in importUserDtos)
            {
                var user = new IdentityUser(dto.UserId, dto.Phone, dto.Email, tenant.Id);
                user.SetPhoneNumber(dto.Phone, true);
                user.SetProperty("Gender", dto.Sex == "男" ? 1 : 0);
                user.SetProperty("Avatar", dto.UserAvatar);
                userList.Add(new Tuple<IdentityUser, string>(user, dto.Password));
            }
        }

        return userList;
    }

}
