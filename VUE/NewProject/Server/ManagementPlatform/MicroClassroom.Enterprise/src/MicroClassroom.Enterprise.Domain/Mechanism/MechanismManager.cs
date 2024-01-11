using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MicroClassroom.Enterprise;

public class MechanismManager : DomainService
{
    private readonly IMechanismRepository _repository;

    public MechanismManager(IMechanismRepository repository)
    {
        _repository = repository;
    }

    public async Task<Mechanism> CreateAsync(string name,
        string pinyin,
        string image,
        string slogo,
        string introduce,
        string about,
        Guid? tenantId)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(pinyin, nameof(pinyin));
        Check.NotNull(image, nameof(image));
        Check.NotNull(slogo, nameof(slogo));

        await ValidateNameAsync(name);

        await ValidatePinyinAsync(pinyin);

        return new Mechanism(GuidGenerator.Create(), name, pinyin, image, slogo, introduce, null, about, tenantId);
    }

    public async Task<Mechanism> UpdateAsync(Guid id,
        string name,
        string pinyin,
        string image,
        string slogo,
        string introduce,
        string about)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(pinyin, nameof(pinyin));
        Check.NotNull(image, nameof(image));
        Check.NotNull(slogo, nameof(slogo));

        var mechanism = await _repository.GetAsync(id);
        if (mechanism.Name != name)
        {
            await ValidateNameAsync(name);
        }

        if (mechanism.Pinyin != pinyin)
        {
            await ValidatePinyinAsync(pinyin);
        }

        mechanism.SetValue(name, pinyin, image, slogo, introduce, null, about);

        return mechanism;
    }

    private async Task ValidateNameAsync(string name, Guid? expectedId = null)
    {
        var mechanism = await _repository.FindAsync(m => m.Name == name);
        if (mechanism != null && mechanism.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Mechanism name: " + name); 
            //TODO: A domain exception would be better..?
        }
    }

    private async Task ValidatePinyinAsync(string pinyin, Guid? expectedId = null)
    {
        var mechanism = await _repository.FindAsync(m => m.Pinyin == pinyin);
        if (mechanism != null && mechanism.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Mechanism Pinyin: " + pinyin);
            //TODO: A domain exception would be better..?
        }
    }
}
