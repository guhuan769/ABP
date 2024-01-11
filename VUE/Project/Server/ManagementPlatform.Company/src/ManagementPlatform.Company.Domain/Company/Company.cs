using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace ManagementPlatform.Company.Company
{
    public class Company : AggregateRoot<Guid>, IMultiTenant
    {
        public Company(Guid id ,string name, string? nameShort, string? nameFullPinyin, string? companyAddress, int? companyClassCode, int? companyTypeCode, string? introduction, long state, int sort, string? remark, DateTime? startAt, DateTime? endAt, Guid? tenantId, List<CompanyItem.CompanyItem> companyItems)
        {
            Id = id;
            Name = name;
            NameShort = nameShort;
            NameFullPinyin = nameFullPinyin;
            CompanyAddress = companyAddress;
            CompanyClassCode = companyClassCode;
            CompanyTypeCode = companyTypeCode;
            Introduction = introduction;
            State = state;
            Sort = sort;
            Remark = remark;
            StartAt = startAt;
            EndAt = endAt;
            TenantId = tenantId;
            CompanyItems = companyItems;
            CompanyItems = new List<CompanyItem.CompanyItem>();
        }

        private Company()
        {
            CompanyItems = new List<CompanyItem.CompanyItem>();
        }

        public string Name { get; private set; }
        public string? NameShort { get; private set; }

        public string? NameFullPinyin { get; private set; }
        public string? CompanyAddress { get; private set; }

        public int? CompanyClassCode { get; private set; }
        public int? CompanyTypeCode { get; private set; }
        public string? Introduction { get; private set; }
        public long State { get; private set; }

        public int Sort { get; private set; }

        public string? Remark { get; private set; }

        public DateTime? StartAt { get; private set; }
        public DateTime? EndAt { get; private set; }

        public Guid? TenantId { get; private set; }

        public virtual List<CompanyItem.CompanyItem> CompanyItems { get; private set; }

        public bool AddCompanyItem(IGuidGenerator guidGenerator, Guid companyItemId)
        {
            var has = CompanyItems.Any(ct => ct.Id == companyItemId);
            if (!has)
            {
                CompanyItems.Add(new CompanyItem.CompanyItem(guidGenerator.Create(),Id, "","","",1,1,"",guidGenerator.Create()));
            }

            return !has;
        }
    }
}
