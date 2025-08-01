using Microsoft.Extensions.Hosting;

namespace FurionTest.Application.Jobs;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine(DateTime.Now);

            // 延迟 1 秒
            await Task.Delay(1000, stoppingToken);
        }
    }
}