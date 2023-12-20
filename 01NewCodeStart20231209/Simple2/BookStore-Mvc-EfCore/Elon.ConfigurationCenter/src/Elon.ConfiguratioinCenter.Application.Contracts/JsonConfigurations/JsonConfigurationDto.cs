using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Elon.ConfiguratioinCenter.JsonConfigurations
{
    public class JsonConfigurationDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Resource { get; set; }
        public ConfigurationType Type { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
