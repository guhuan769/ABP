using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Notices
{
    public class Notice:AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public NoticeType type { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
