using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

public class GetTeacherInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
