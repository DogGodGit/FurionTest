namespace FurionTest.Api;

[ApiDescriptionSettings("Test", Tag = "Job")]
public class JobController : IDynamicApiController
{
    private IJobService _yourService;

    public JobController(IJobService yourService)
    {
        _yourService = yourService;
    }

    public void AddJob()
    {
        _yourService.AddJob();
    }

    public void RemoveJob()
    {
        _yourService.RemoveJob();
    }

    public void SyncTask()
    {
        _yourService.SyncTask();
    }

    public void SyncTask2()
    {
        _yourService.SyncTask2();
    }

    public Task AsyncTask()
    {
        return _yourService.AsyncTask();
    }

    public Task AsyncTask2()
    {
        return _yourService.AsyncTask2();
    }

    public void SyncTask3()
    {
        _yourService.SyncTask3();
    }

    public Task AsyncTask3()
    {
        return _yourService.AsyncTask3();
    }
}