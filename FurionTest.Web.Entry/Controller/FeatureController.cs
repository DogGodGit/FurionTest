using FurionTest.BAS.Feature;

namespace FurionTest.Api;

[ApiDescriptionSettings("MultiService", Tag = "MultiService")]
public class FeatureController : IDynamicApiController
{
    private IFeatureService _featureService;

    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    public string GetName()
    {
        return _featureService.ExecuteFeature();
    }
}