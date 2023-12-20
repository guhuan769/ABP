using Microsoft.AspNetCore.Builder;
using Elon.ForumABPExample;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<ForumABPExampleWebTestModule>();

public partial class Program
{
}
