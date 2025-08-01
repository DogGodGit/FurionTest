public interface IJobService
{
    void AddJob();

    void SyncTask();

    void SyncTask2();

    Task AsyncTask();

    Task AsyncTask2();

    void SyncTask3();

    Task AsyncTask3();
    void RemoveJob();
}