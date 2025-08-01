using Furion.Schedule;
using Furion.TaskQueue;
using Furion.TimeCrontab;

public class JobService : IJobService, ITransient
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ITaskQueue _taskQueue;

    public JobService(ISchedulerFactory schedulerFactory, ITaskQueue taskQueue)
    {
        _schedulerFactory = schedulerFactory;
        _taskQueue = taskQueue;
    }

    public void AddJob()
    {
        _schedulerFactory.AddJob<MyJob>("动态作业 Id", Triggers.Secondly());
    }

    public void RemoveJob()
    {
        _schedulerFactory.RemoveJob("动态作业 Id");
    }

    /// <summary>
    /// 同步入队
    /// </summary>
    public void SyncTask()
    {
        _taskQueue.Enqueue(provider =>
        {
            Console.WriteLine("我是同步的");
        });

        // 如无需使用 provider 参数，可用 _ 替代
        _taskQueue.Enqueue(_ => { });
    }

    /// <summary>
    /// 同步入队，延迟 3 秒触发
    /// </summary>
    public void SyncTask2()
    {
        _taskQueue.Enqueue(provider =>
        {
            Console.WriteLine("我是同步的，但我延迟了 3 秒");
        }, 3000);

        // 如无需使用 provider 参数，可用 _ 替代
        _taskQueue.Enqueue(_ => { }, 3000);
    }

    /// <summary>
    /// 异步入队
    /// </summary>
    public async Task AsyncTask()
    {
        await _taskQueue.EnqueueAsync(async (provider, token) =>
        {
            Console.WriteLine("我是异步的");
            await ValueTask.CompletedTask;
        });

        // 如无需使用 provider 和 token 参数，可用 _ 替代
        await _taskQueue.EnqueueAsync(async (_, _) => { });
    }

    /// <summary>
    /// 异步入队，延迟 3 秒触发
    /// </summary>
    public async Task AsyncTask2()
    {
        await _taskQueue.EnqueueAsync(async (provider, token) =>
        {
            Console.WriteLine("我是异步的，但我延迟了 3 秒");
            await ValueTask.CompletedTask;
        }, 3000);

        // 如无需使用 provider 和 token 参数，可用 _ 替代
        await _taskQueue.EnqueueAsync(async (_, _) => { }, 3000);
    }

    /// <summary>
    /// 同步入队，支持 Cron 表达式延迟
    /// </summary>
    public void SyncTask3()
    {
        _taskQueue.Enqueue(provider =>
        {
            Console.WriteLine("Cron ...");
        }, "* * * * *");

        // 如无需使用 provider 参数，可用 _ 替代
        _taskQueue.Enqueue(_ => { }, "* * * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 异步入队，支持 Cron 表达式延迟
    /// </summary>
    public async Task AsyncTask3()
    {
        await _taskQueue.EnqueueAsync(async (provider, token) =>
        {
            Console.WriteLine("Cron ...");
            await ValueTask.CompletedTask;
        }, "* * * * *");

        // 如无需使用 provider 和 token 参数，可用 _ 替代
        await _taskQueue.EnqueueAsync(async (_, _) => { }, "* * * * *", CronStringFormat.Default);
    }
}