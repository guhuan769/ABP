using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ManagementPlatform.Company.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
