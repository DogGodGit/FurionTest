using FurionTest.Application.Services.ToDoEvent;
using FurionTest.Application.Services.ToDoEvent.EventSourceStorer;
using FurionTest.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace FurionTest.Web.Core;

[AppStartup(10)]
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter();

        services.AddSqlSugar(GlobalConfig.ConnectionConfigs.ToArray(), buildAction: db =>
        {
            // 配置全局事件，比如拦截执行 SQL
            if (GlobalConfig.IsSqlProfiler)
            {
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    if (GlobalConfig.IsSqlProfiler)
                    {
                        string outPutSql = SqlProfiler.ParameterFormat(sql, pars);
                        outPutSql.LogDebug();
                    }
                };
            }

            db.Aop.OnError = (exp) =>
            {
                string outPutSql = SqlProfiler.ParameterFormat(exp.Sql, exp.Parametres);
                outPutSql.LogError();
            };
        });

        if (GlobalConfig.CacheType == CacheType.SqlServer)
        {
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = GlobalConfig.ConnectionConfigs[0].ConnectionString;
                options.SchemaName = "dbo";
                options.TableName = GlobalConfig.ConnectionConfigs[0].DbLinkName;
            });
        }
        else if (GlobalConfig.CacheType == CacheType.Redis)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                // 连接字符串，这里也可以读取配置文件
                options.Configuration = GlobalConfig.Redis.ConnectionString;
                // 键名前缀
                options.InstanceName = GlobalConfig.Redis.IndexSuffix;
            });
        }

        services.AddJwt<JwtHandler>(enableGlobalAuthorize: GlobalConfig.EnableGlobalAuthorize);

        services.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//处理乱码问题
            options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss");
            options.JsonSerializerOptions.IncludeFields = true;//包含成员字段序列化
            options.JsonSerializerOptions.AllowTrailingCommas = true;//允许尾随逗号
            options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;//允许注释
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;//不区分大小写
        });

        services.AddFileLogging("logs/{0:yyyy}-{0:MM}-{0:dd}.log", options =>
        {
            options.FileNameRule = fileName =>
            {
                return string.Format(fileName, DateTime.Now);
            };
            options.WriteFilter = (logMsg) =>
            {
                return logMsg.LogLevel >= LogLevel.Information;
            };
        });

        services.AddRemoteRequest(options =>
        {
            // 配置 Github 基本信息
            options.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            })
            .ConfigurePrimaryHttpMessageHandler(u => new HttpClientHandler
            {
                // 忽略 SSL 不安全检查，或 https 不安全或 https 证书有误
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ClientCertificates = {
                    new X509Certificate2("...","..."),
                    new X509Certificate2("...","..."),
                    new X509Certificate2("...","...")
                }
            });

            // 配置 Facebook 基本信息
            options.AddHttpClient("facebook", c =>
            {
                c.BaseAddress = new Uri("https://api.facebook.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.facebook.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });
        });

        services.AddEventBus(options =>
        {
            // 注册 ToDo 事件订阅者
            options.AddSubscriber<ToDoEventSubscriber>();

            if (GlobalConfig.EventHubType == EventHubType.Redis)
            {
                // 替换事件源存储器
                options.ReplaceStorer(serviceProvider =>
                {
                    var redisClient = new RedisHelper(GlobalConfig.Redis.ConnectionString, GlobalConfig.Redis.IndexSuffix);
                    return new RedisEventSourceStorer(redisClient);
                });
            }
            else if (GlobalConfig.EventHubType == EventHubType.RabbitMQ)
            {
                // 替换事件源存储器
                options.ReplaceStorer(serviceProvider =>
                {
                    // 创建连接工厂
                    var factory = new ConnectionFactory
                    {
                        HostName = GlobalConfig.RabbitMQ.HostName,
                        UserName = GlobalConfig.RabbitMQ.UserName,
                        Password = GlobalConfig.RabbitMQ.Password,
                    };
                    // 创建默认内存通道事件源对象，可自定义队列路由key，比如这里是 eventbus
                    return new RabbitMQEventSourceStorer(factory);
                });
            }

            // 替换事件源存储器
            options.ReplacePublisher<ToDoEventPublisher>();

            // 添加事件执行监视器
            options.AddMonitor<ToDoEventHandlerMonitor>();

            // 添加事件执行器
            options.AddExecutor<RetryEventHandlerExecutor>();

            // 订阅 EventBus 未捕获异常
            options.UnobservedTaskExceptionHandler = (obj, args) =>
            {
                Log.Error("Error", obj);
            };
        });

        services.AddSensitiveDetection();

        services.AddClassesMatchingInterfaces();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}