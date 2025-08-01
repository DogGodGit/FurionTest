using Furion.DependencyInjection;

namespace FurionTest.BAS.Feature;

public class StandardFeatureService : IFeatureService, ITransient
{
    public string ExecuteFeature()
    {
        return "我是：" + nameof(StandardFeatureService);
    }
}