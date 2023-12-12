using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Notices
{
    public class CreateUpdateNoticeDto : AuditedEntityDto<Guid>
    {

        [Required]
        [StringLength(NoticeConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(NoticeConsts.MaxContentLength)]
        public string Content { get; set; }

        [Required]
        public NoticeType Type { get; set; } = NoticeType.System;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;
      
    }
}
