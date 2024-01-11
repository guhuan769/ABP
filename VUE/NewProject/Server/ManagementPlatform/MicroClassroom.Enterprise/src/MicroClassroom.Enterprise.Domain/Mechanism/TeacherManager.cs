using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MicroClassroom.Enterprise;

public class TeacherManager : DomainService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMechanismRepository _mechanismRepository;

    public TeacherManager(ITeacherRepository teacherRepository,
        IMechanismRepository mechanismRepository)
    {
        _teacherRepository = teacherRepository;
        _mechanismRepository = mechanismRepository;
    }

    public async Task<Teacher> CreateAsync(string name,
        string image,
        string introduce,
        Guid? tenantId)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(image, nameof(image));

        await ValidateNameAsync(name);

        var mechanism = await _mechanismRepository.GetSingleAsync();

        return new Teacher(GuidGenerator.Create(), mechanism.Id, name, image, introduce, tenantId);
    }

    public async Task<Teacher> UpdateAsync(Guid id,
        string name,
        string image,
        string introduce)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(image, nameof(image));

        var teacher = await _teacherRepository.GetAsync(id);

        if (teacher.Name != name)
        {
            await ValidateNameAsync(name);
        }
      
        teacher.SetValue(name, image, introduce);

        return teacher;
    }

    private async Task ValidateNameAsync(string name, Guid? expectedId = null)
    {
        var teacher = await _teacherRepository.FindAsync(m => m.Name == name);
        if (teacher != null && teacher.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Teacher name: " + name);
            //TODO: A domain exception would be better..?
        }
    }
}
