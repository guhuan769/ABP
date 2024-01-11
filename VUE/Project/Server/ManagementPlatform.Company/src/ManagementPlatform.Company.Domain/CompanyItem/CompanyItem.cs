using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace ManagementPlatform.Company.CompanyItem
{
    public class CompanyItem : Entity<Guid>, IMultiTenant
    {
        public CompanyItem(Guid id, Guid CompanyId, string name, string nameShort, string nameFullPinyin, int state, int sort, string? remark, Guid? tenantId)
        {
            Id = id;
            CompanyId = CompanyId;
            Name = name;
            NameShort = nameShort;
            NameFullPinyin = nameFullPinyin;
            State = state;
            Sort = sort;
            Remark = remark;
            TenantId = tenantId;
        }

        public Guid CompanyId { get; private set; }
        public string Name { get; private set; }
        public string NameShort { get; private set; }
        public string NameFullPinyin { get; private set; }

        public int State { get; private set; }

        public int Sort { get; private set; }
        public string? Remark { get; private set; }

        public Guid? TenantId { get; private set; }
    }
}
