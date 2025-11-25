using FurionTest.Application.Interfaces;
using FurionTest.Application.Proxy;
using FurionTest.Core;
using FurionTest.Core.Models;

namespace FurionTest.Application.Services;

[Injection(Proxy = typeof(LogDispatchProxy))]
public class TestService : ITestService, ITransient
{
    private ISqlSugarClient db;
    private int total;

    public TestService(ISqlSugarClient db)
    {
        this.db = db;
    }

    public string SayHello(string word)
    {
        return $"Hello {word}";
    }

    public ViewOrder GetOrder(string id)
    {
        var entity = db.Queryable<Order>().First();
        var dto = entity.Adapt<ViewOrder>();

        return dto;
    }

    public void QueryAllList()
    {
        //查询所有
        var getAll = db.Queryable<Student>().ToList();
        //查询前10
        var top10 = db.Queryable<Student>().Take(10).ToList();
        //查询单条
        var getFirst = db.Queryable<Student>().First(it => it.Id == 1);
        //with nolock
        var getAllNoLock = db.Queryable<Student>().With(SqlWith.NoLock).ToList();
        //根据主键查询
        var getByPrimaryKey = db.Queryable<Student>().InSingle(2);
        //查询总和
        var sum = db.Queryable<Student>().Sum(it => it.Id);
        //是否存在
        var isAny = db.Queryable<Student>().Where(it => it.Id == -1).Any();
        //模糊查
        var list2 = db.Queryable<Order>().Where(it => it.Name.Contains("jack")).ToList();

        var list = db.Queryable<Student, School>((st, sc) => new JoinQueryInfos(
                JoinType.Left, st.SchoolId == sc.Id))
              .Select((st, sc) => new { Id = st.Id, Name = st.Name, SchoolName = sc.Name }).ToList();

        int pageIndex = 1;
        int pageSize = 20;
        int totalCount = 0;
        var page = db.Queryable<Student>().ToPageList(pageIndex, pageSize, ref totalCount);

        //sql分页
        var list1 = db.SqlQueryable<Student>("select * from student").ToPageList(1, 2, ref total);

        //原生Sql用法
        var dt = db.Ado.GetDataTable("select * from student where StudentID=@id and name=@name", new List<SugarParameter>(){
          new SugarParameter("@id",1),
          new SugarParameter("@name",2)
        });
        //参数2
        dt = db.Ado.GetDataTable("select * from student where StudentID=@id and name=@name", new { id = 1, name = 2 });

        if (db.CurrentConnectionConfig.DbType == DbType.SqlServer)
        {
            //存储过程用法
            var nameP = new SugarParameter("@name", "张三");
            var ageP = new SugarParameter("@age", null, true);//设置为output
            dt = db.Ado.UseStoredProcedure().GetDataTable("sp_school", nameP, ageP);
        }
    }

    public void InsertStudent()
    {
        var insertObj = new Student()
        {
            SchoolId = 1,
            Name = "张三"
        };
        //可以是 类 或者 List<类>
        db.Insertable(insertObj).ExecuteCommand();

        //插入返回自增列
        db.Insertable(insertObj).ExecuteReturnIdentity();

        //可以是 Dictionary 或者 List<Dictionary >
        var dc = new Dictionary<string, object>();
        dc.Add("name", "1");
        dc.Add("CreateTime", db.GetDate());
        db.Insertable(dc).AS("student").ExecuteCommand();
    }

    public void UpdateStudent()
    {
        //更新单条
        var updateObj = new Student()
        {
            Id = 1,
            Name = "李四"
        };
        db.Updateable(updateObj).ExecuteCommand();
        //更新多条
        var updateList = new List<Student>()
        {
            new Student() { Id = 1, Name = "李四" },
            new Student() { Id = 2, Name = "王五" }
        };
        db.Updateable(updateList).ExecuteCommand();
        db.Updateable(updateList).UpdateColumns(x => x.CreateTime == DateTime.Now).ExecuteCommand();

        //不更新 Name 和TestId
        var result = db.Updateable(updateObj).IgnoreColumns(it => new { it.CreateTime, it.TestId }).ExecuteCommand();
        //只更新 Name 和 CreateTime
        result = db.Updateable(updateObj).UpdateColumns(it => new { it.Name, it.CreateTime }).ExecuteCommand();
        //根据表达式更新
        result = db.Updateable<Order>()
                      .SetColumns(it => it.Name == "a")
                    .Where(it => it.Id == 11).ExecuteCommand();
    }

    public void DeleteStudent()
    {
        //根据实体删除
        db.Deleteable<Student>().Where(new Student() { Id = 1 }).ExecuteCommand();
        //根据主键删除
        db.Deleteable<Student>().In(1).ExecuteCommand();
        //根据表达式删除
        db.Deleteable<Student>().Where(it => it.Id == 1).ExecuteCommand();
    }

    public List<ViewOrder> GetOrderList()
    {
        //2.手动获取
        App.GetService<ISqlSugarClient>();

        var query5 = DbContext.Instance.Queryable<Order>()
            .LeftJoin<Custom>((o, cus) => o.CustomId == cus.Id)
            .LeftJoin<OrderItem>((o, cus, oritem) => o.Id == oritem.OrderId)
            .Where(o => o.Id == 1)
            .Select((o, cus) => new ViewOrder { Id = o.Id.ToString(), CustomName = cus.Name })
            .ToList();
        return query5;
    }

    public bool BulkList()
    {
        var lis2t = db.Queryable<SplitTestTable>()
            .SplitTable(DateTime.Now.Date.AddYears(-1), DateTime.Now)
            .ToPageList(1, 2);

        //Insert A million only takes a few seconds
        db.Fastest<Order>().BulkCopy(GetList());

        //update A million only takes a few seconds
        db.Fastest<Order>().BulkUpdate(GetList());//A million only takes a few seconds完
        db.Fastest<Order>().BulkUpdate(GetList(), new string[] { "Id" }, new string[] { "name", "CreateTime" });//no primary key

        //if exists update, else  insert
        var x = db.Storageable(GetList()).ToStorage();
        x.BulkCopy();
        x.BulkUpdate();

        //set table name
        db.Fastest<Order>().AS("Order").BulkCopy(GetList());

        //set page
        db.Fastest<Order>().PageSize(300000).BulkCopy(GetList());

        return true;
    }

    private List<Order> GetList()
    {
        List<Order> list = new List<Order>();
        for (int i = 0; i < 100; i++)
        {
            list.Add(new Order() { CreateTime = DateTime.Now, CustomId = 1, Name = "Test", Price = 100 });
        }

        return list;
    }

    public bool ValidateData()
    {
        return true;
    }
}