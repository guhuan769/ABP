using MicroClassroom.Enterprise.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MicroClassroom.Enterprise;

public abstract class EnterpriseController : AbpControllerBase
{
    protected EnterpriseController()
    {
        LocalizationResource = typeof(EnterpriseResource);
    }
}
