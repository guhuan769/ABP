using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Elon.ConfiguratioinCenter.JsonConfigurations
{
    public class JsonConfigurationAppService :
    CrudAppService<
        JsonConfiguration, //The Book entity
        JsonConfigurationDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateJsonConfigurationDto>
        , IJsonConfigurationAppService,ITransientDependency
    {
        public JsonConfigurationAppService(IRepository<JsonConfiguration, Guid> repository) : base(repository)
        {
        }

        public async Task Restore(int number)
        {
            Console.WriteLine($"This is {nameof(JsonConfigurationAppService)},{nameof(Restore)} {number}");
            await Task.Delay(0);
        }
    }
}
