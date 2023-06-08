using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Notices
{
    public class CreateUpdateNoticeDto:AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(NoticeConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(NoticeConsts.MaxContentLength)]
        public string Content { get; set; }
        public NoticeType type { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
