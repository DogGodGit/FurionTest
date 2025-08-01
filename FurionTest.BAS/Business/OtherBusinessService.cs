using FurionTest.Common;
using FurionTest.Core.Models;
using SqlSugar;

namespace FurionTest.BAS.Business;

[MultiServiceFilter(RegionType.Asia)]
public class OtherBusinessService : BusinessService
{
    public OtherBusinessService(ISqlSugarClient db) : base(db)
    {
    }

    public Person Get(int id)
    {
        return new Person { Id = id };
    }

    public override string GetName()
    {
        return "我是：" + nameof(OtherBusinessService);
    }
}