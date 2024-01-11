﻿using Volo.Abp.Modularity;

namespace ManagementPlatform.Identity;

public abstract class IdentityApplicationTestBase<TStartupModule> : IdentityTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
