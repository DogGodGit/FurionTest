using FurionTest.BAS.Business;
using FurionTest.Core.Models;

namespace FurionTest.Api;

[ApiDescriptionSettings("MultiService", Tag = "MultiService")]
public class BusinessController : IDynamicApiController
{
    private IBusinessService _businessService;

    public BusinessController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    public Person Get(int id)
    {
        return _businessService.Get(id);
    }

    public string GetName()
    {
        return _businessService.GetName();
    }
}