using Elon.DashboardCenter.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IOC 注册 
await builder.AddApplicationAsync<DashboardCenterWebModule>();

var app = builder.Build();
await app.InitializeApplicationAsync(); //初始化进程 -- 不是配置httpipeline 管道模型的吗
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
