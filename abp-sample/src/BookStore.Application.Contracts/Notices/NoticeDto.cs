using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Notices
{
    /// <summary>
    /// 贫血模型
    /// </summary>
    public class NoticeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public NoticeType type { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
