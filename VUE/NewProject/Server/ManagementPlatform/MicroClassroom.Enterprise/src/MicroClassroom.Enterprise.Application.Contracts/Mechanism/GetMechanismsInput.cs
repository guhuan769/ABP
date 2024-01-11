using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

public class GetMechanismsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}