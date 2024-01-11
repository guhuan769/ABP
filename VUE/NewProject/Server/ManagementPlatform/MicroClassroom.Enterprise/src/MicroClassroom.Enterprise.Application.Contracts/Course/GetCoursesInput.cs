using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

public class GetCoursesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
