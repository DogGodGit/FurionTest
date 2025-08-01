using SqlSugar;
using System;
using System.Collections.Generic;

namespace FurionTest.Core
{
    /// <summary>
    /// 数据库上下文对象
    /// </summary>
    public static class DbContext
    {
        /// <summary>
        /// SqlSugar 数据库实例
        /// </summary>
        public static readonly SqlSugarScope Instance = new(
            // 读取 appsettings.json 中的 ConnectionConfigs 配置节点
            GlobalConfig.ConnectionConfigs
            , db =>
            {
                // 这里配置全局事件，比如拦截执行 SQL
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    if (GlobalConfig.IsSqlProfiler)
                    {
                        string outPutSql = SqlProfiler.ParameterFormat(sql, pars);
                        Console.WriteLine(outPutSql);
                    }
                };

                db.Aop.OnError = (exp) =>
                {
                    string outPutSql = SqlProfiler.ParameterFormat(exp.Sql, exp.Parametres);
                    Console.WriteLine(outPutSql);
                };
            });



        public static void GetConnectList()
        {
            List<ConnectionConfig> connectConfigList = new List<ConnectionConfig>();
            //数据库1
            connectConfigList.Add(new ConnectionConfig
            {
                ConnectionString = "链接字符串1",
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigId = "0",
                AopEvents = new AopEvents
                {
                    //多库状态下每个库必须单独绑定打印事件，否则只会打印第一个库的sql日志
                    OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(SqlProfiler.ParameterFormat(sql, pars));
                        Console.WriteLine();
                    }
                }
            });
            //数据库2
            connectConfigList.Add(new ConnectionConfig
            {
                ConnectionString = "链接字符串2",
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigId = "1",
                AopEvents = new AopEvents
                {
                    //多库状态下每个库必须单独绑定打印事件，否则只会打印第一个库的sql日志
                    OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(SqlProfiler.ParameterFormat(sql, pars));
                        Console.WriteLine();
                    }
                }
            });
        }
    }
}