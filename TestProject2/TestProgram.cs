using Furion.Logging.Extensions;
using Furion.Xunit;
using FurionTest.Core;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Xunit.Abstractions;
using Xunit.Sdk;

// 配置启动类类型，第一个参数是Startup类完整限定名，第二个参数是当前项目程序集名称
[assembly: TestFramework("TestProject2.TestProgram", "TestProject2")]

namespace TestProject2;

/// <summary>
/// 单元测试启动类
/// </summary>
/// <remarks>在这里可以使用Furion几乎所有功能</remarks>
public sealed class TestProgram : TestStartup
{
    public TestProgram(IMessageSink messageSink) : base(messageSink)
    {
        Serve.RunNative(services =>
        {
            // 注册远程服务
            services.AddRemoteRequest();
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
        });
    }
}