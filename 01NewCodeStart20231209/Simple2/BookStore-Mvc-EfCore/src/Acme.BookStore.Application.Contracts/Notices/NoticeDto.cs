using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Notices
{
    public class NoticeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public NoticeType Type { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
