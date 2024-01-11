using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

public class GetBannerInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
