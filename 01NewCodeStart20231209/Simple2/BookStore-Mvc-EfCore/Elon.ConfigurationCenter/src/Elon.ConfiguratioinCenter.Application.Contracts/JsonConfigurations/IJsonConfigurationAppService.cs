using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Elon.ConfiguratioinCenter.JsonConfigurations
{
    public interface IJsonConfigurationAppService :
    ICrudAppService< //Defines CRUD methods
        JsonConfigurationDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateJsonConfigurationDto>
    {
        /// <summary>
        /// 重新存储
        /// </summary>
        /// <returns></returns>
        public Task Restore(int number);
    }
}
