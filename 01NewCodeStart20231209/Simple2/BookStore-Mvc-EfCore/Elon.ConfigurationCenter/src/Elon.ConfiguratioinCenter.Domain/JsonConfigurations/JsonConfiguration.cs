using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Elon.ConfiguratioinCenter.JsonConfigurations
{
    public class JsonConfiguration:AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Resource { get; set; }
        public ConfigurationType Type { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
