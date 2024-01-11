using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

/// <summary>
/// Banner
/// </summary>
public class Banner : Entity<Guid>, IMultiTenant
{
    private Banner() { }

    public Banner(Guid id, Guid mechanismId,string title, string image, Guid? tenantId)
    {
        Id = id;
        MechanismId = mechanismId;
        Title = title;
        Image = image;
        TenantId = tenantId;
    }

    public Guid MechanismId { get; private set; }

    public string Title { get; private set; }

    public string Image { get; private set; }

    public Guid? TenantId { get; private set; }

    public void SetValue(Guid mechanismId, string title, string image)
    {
        MechanismId = mechanismId;
        Title = title;
        Image = image;
    }

    public void SetValue(string title, string image)
    {
        Title = title;
        Image = image;
    }
}
