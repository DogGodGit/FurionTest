using SqlSugar;
using System;
using System.Reflection;
using System.Linq;

public class AppDbContext
{
    public SqlSugarClient Db;

    public AppDbContext(string connectionString, DbType DbType = DbType.SqlServer, bool IsAutoCloseConnection = true)
    {
        Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = connectionString,
            DbType = DbType,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,

            MoreSettings = new ConnMoreSettings()
            {
                EnableCodeFirstUpdatePrecision = true,
            },
            ConfigureExternalServices = new ConfigureExternalServices
            {
                //注意:  这儿AOP设置不能少
                EntityService = (c, p) =>
                {
                    /***低版本C#写法***/
                    // int?  decimal?这种 isnullable=true 不支持string(下面.NET 7支持)
                    if (p.IsPrimarykey == false && c.PropertyType.IsGenericType &&
                    c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        p.IsNullable = true;
                    }

                    /***高版C#写法***/
                    //支持string?和string  
                    if (p.IsPrimarykey == false && new NullabilityInfoContext()
                     .Create(c).WriteState is NullabilityState.Nullable)
                    {
                        p.IsNullable = true;
                    }

                    //p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);//ToUnderLine驼峰转下划线方法
                },

                EntityNameService = (x, p) => //处理表名
                {
                    //p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName);//ToUnderLine驼峰转下划线方法
                }
            }
        });
    }

    public void CreateTable(bool Backup = false, int StringDefaultLength = 50, params Type[] types)
    {
        Db.CodeFirst.SetStringDefaultLength(StringDefaultLength);
        Db.DbMaintenance.CreateDatabase();
        if (Backup)
        {
            Db.CodeFirst.BackupTable().InitTables(types);
        }
        else
        {
            /***批量创建表***/
            //语法1：
            if (!types.Any()) types = [.. typeof(AppDbContext).Assembly.GetTypes().Where(it => it.FullName.Contains("Models"))];

            Db.CodeFirst.InitTables(types);
        }
    }
}

public static class SqlSugarExtensions
{
    public static void CreateTable(this ISqlSugarClient db, bool Backup = false, int StringDefaultLength = 50, params Type[] types)
    {
        db.CodeFirst.SetStringDefaultLength(StringDefaultLength);
        db.DbMaintenance.CreateDatabase();
        if (Backup)
        {
            db.CodeFirst.BackupTable().InitTables(types);
        }
        else
        {
            /***批量创建表***/
            //语法1：
            if (!types.Any()) types = [.. typeof(AppDbContext).Assembly.GetTypes().Where(it => it.FullName.Contains("Models"))];

            db.CodeFirst.InitTables(types);
        }
    }
}