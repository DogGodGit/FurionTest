using Furion.DependencyInjection;
using FurionTest.Core.Models;
using SqlSugar;

namespace FurionTest.BAS.Business;
public class BusinessService : IBusinessService, ITransient
{
    private ISqlSugarClient _db;

    public BusinessService(ISqlSugarClient db)
    {
        _db = db;
    }

    public Person Get(int id)
    {
        return _db.Queryable<Person>().First(m => m.Id == id);
    }

    public virtual string GetName()
    {
        return "我是：" + nameof(BusinessService);
    }
}