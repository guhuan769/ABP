﻿using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace ManagementPlatform.Production;

[DependsOn(
    typeof(ProductionDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ProductionApplicationContractsModule : AbpModule
{

}
