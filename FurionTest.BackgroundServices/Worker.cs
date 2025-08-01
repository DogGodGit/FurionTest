using Furion.TimeCrontab;

namespace FurionTest.BackgroundServices;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private const int delay = 1000;
    private readonly Crontab _crontab;
    private bool _isLock = false;

    // 服务工厂
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _crontab = Crontab.Parse("* * * * * *", CronStringFormat.WithSeconds);
        _scopeFactory = scopeFactory;
    }

    // 启动
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    // 执行逻辑
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        // 放在循环外可以避免高频下频繁创建作用域和解析服务
        using var scope = _scopeFactory.CreateScope();
        var services = scope.ServiceProvider;

        while (!stoppingToken.IsCancellationRequested)
        {
            // 放在循环内针对频率不是很高的操作
            // using var scope = _scopeFactory.CreateScope();
            // var services = scope.ServiceProvider;

            //// 获取数据库上下文
            //var dbContext = Db.GetDbContext(services);
            //// 获取仓储
            //var respository = Db.GetRepository<Person>(services);
            // 解析其他服务



            if (_isLock) goto next;
            _isLock = true;

            await Task.Delay(delay, stoppingToken);
            await Task.Delay(_crontab.GetSleepTimeSpan(DateTime.Now), stoppingToken);

            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var task = await taskFactory.StartNew(async () =>
            {
                // 模拟耗时操作
                await Task.Delay(2000);

                _logger.LogInformation("Worker running at: {time}", DateTime.Now);

                await Task.CompletedTask;
            }, stoppingToken);
            // 等待任务完成
            await task.ContinueWith(task => _isLock = false);

            next:
            await Task.Delay(_crontab.GetSleepTimeSpan(DateTime.Now), stoppingToken);
        }
    }

    // 停止
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return base.StopAsync(cancellationToken);
    }
}