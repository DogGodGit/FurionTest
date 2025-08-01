using Furion.Schedule;
using Furion.TimeCrontab;
using Microsoft.Extensions.Logging;

[Minutely]
[Cron("3,7,8 * * * * ?", CronStringFormat.WithSeconds)]
public class MyJob : IJob
{
    private readonly ILogger<MyJob> _logger;
    public MyJob(ILogger<MyJob> logger)
    {
        _logger = logger;
    }

    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        //_logger.LogInformation($"{context.ConvertToJSON()}");
        //_logger.LogInformation($"{context.JobDetail.ConvertToJSON()}");
        //_logger.LogInformation($"{context.Trigger.ConvertToJSON(NamingConventions.UnderScoreCase)}");

        //var jobDetail = context.JobDetail;
        //var trigger = context.Trigger;

        //_logger.LogInformation(jobDetail.ConvertToSQL("作业信息表名", PersistenceBehavior.Appended));  // 输出新增语句
        //_logger.LogInformation(trigger.ConvertToSQL("作业触发器表名", PersistenceBehavior.Removed, NamingConventions.Pascal));    // 输出删除语句
        //_logger.LogInformation(trigger.ConvertToSQL("作业触发器表名", PersistenceBehavior.Updated, NamingConventions.UnderScoreCase));    // 输出更新语句

        _logger.LogInformation(context.JobDetail.ConvertToMonitor());
        _logger.LogInformation(context.Trigger.ConvertToMonitor());

        await Task.Delay(1000, stoppingToken); // 模拟耗时操作
    }
}
