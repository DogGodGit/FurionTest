using Furion.DependencyInjection;
using FurionTest.Common;

namespace FurionTest.BAS.Feature;

[MultiServiceFilter(RegionType.NorthAmerica)]
public class NewFeatureService : IFeatureService, ITransient
{
    public string ExecuteFeature()
    {
        return "我是：" + nameof(NewFeatureService);
    }
}
