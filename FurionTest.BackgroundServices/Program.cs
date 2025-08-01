using Furion;
using Furion.Schedule;
using Furion.TimeCrontab;
using FurionTest.BackgroundServices;

var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IJobService, JobService>();

// 可以完全动态操作，只需要注册服务即可
builder.Services.AddSchedule();

builder.Services.AddTaskQueue();
//builder.Services.AddSchedule(options =>
//{
//    // 注册作业，并配置作业触发器
//    //options.AddJob<MyJob>(Triggers.Secondly()); // 表示每秒执行
//    //options.AddJob<MyJob>("myjob", Triggers.Secondly());
//    options.AddJob<MyJob>(Triggers.Minutely()   // 每分钟开始
//     , Triggers.Period(5000)   // 每 5 秒，还支持 Triggers.PeriodSeconds(5)，Triggers.PeriodMinutes(5)，Triggers.PeriodHours(5)
//     , Triggers.Cron("3,7,8 * * * * ?", CronStringFormat.WithSeconds));  // 每分钟第 3/7/8 秒
//    options.AddJob<MyJob>(concurrent: false, Triggers.Secondly()); // 串行，每秒执行
//    options.LogEnabled = false;
//});

//builder.Services.AddSchedule(options =>
//{
//    options.AddJob(JobBuilder.Create<MyJob>().SetIncludeAnnotations(true)
//        , Triggers.PeriodSeconds(5));     // 这里可传可不传，传了则会自动载入特性和这里配置的作业触发器

//    // 还可以更简单~~
//    options.AddJob(typeof(MyJob).ScanToBuilder());

//    // 还可以批量新增 Furion 4.8.2.4+
//    //options.AddJob(App.EffectiveTypes.ScanToBuilders());
//});

var host = builder.Build();
host.Run();
