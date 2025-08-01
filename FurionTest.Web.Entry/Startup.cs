using FurionTest.Api.Hubs;

namespace FurionTest.Api;

[AppStartup(99)]
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCorsAccessor();

        services.AddControllers()
                .AddAppLocalization()  // 注册多语言
                .AddInjectWithUnifyResult();

        // 添加即时通讯
        services.AddSignalR();

        services.AddSchedule();

        services.AddTaskQueue();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();
        // 配置多语言，必须在 路由注册之前
        app.UseAppLocalization();
        app.UseInject(string.Empty);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // 注册集线器
            endpoints.MapHub<ChatHub>("/chatHub");
        });
    }
}