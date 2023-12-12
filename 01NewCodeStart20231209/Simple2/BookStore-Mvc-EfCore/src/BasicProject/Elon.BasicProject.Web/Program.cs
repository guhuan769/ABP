using Elon.BasicProject.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Host.AddAppSettingsSecretsJson()
              .UseAutofac(); 
//IOC 注册 
await builder.AddApplicationAsync<BaseProjectWebModule>();

var app = builder.Build();
await app.InitializeApplicationAsync(); //初始化进程 -- 不是配置httpipeline 管道模型的吗
app.MapRazorPages();
await app.RunAsync();
// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();
