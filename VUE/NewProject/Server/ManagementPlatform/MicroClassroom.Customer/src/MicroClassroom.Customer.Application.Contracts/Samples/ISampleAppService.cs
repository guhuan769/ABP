using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Customer.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
