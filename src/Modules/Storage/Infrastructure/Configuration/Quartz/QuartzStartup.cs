using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.InternalCommands;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Outbox;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Quartz
{
    /// <summary>
    /// Quartz startup class.
    /// </summary>
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        /// <summary>
        /// Initializes quartz scheduling.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        internal static void Initialize(ILogger logger)
        {
            logger.LogInformation("Quartz starting...");

            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Meetings");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            //LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            _scheduler.Start().GetAwaiter().GetResult();

            var tempFileCleanupJob = JobBuilder.Create<CleanupTempFilesJob>().Build();
            var triggerTempFileCleanup =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .Build();

            _scheduler
                .ScheduleJob(tempFileCleanupJob, triggerTempFileCleanup)
                .GetAwaiter().GetResult();


            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var triggerOutboxProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .Build();

            _scheduler
                .ScheduleJob(processOutboxJob, triggerOutboxProcessing)
                .GetAwaiter().GetResult();

            //var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            //var processInboxTrigger =
            //    TriggerBuilder
            //        .Create()
            //        .StartNow()
            //        .WithCronSchedule("0/2 * * ? * *")
            //        .Build();

            //_scheduler
            //    .ScheduleJob(processInboxJob, processInboxTrigger)
            //    .GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .Build();
            _scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();

            //logger.Information("Quartz started.");
        }

        internal static void StopQuartz()
        {
            _scheduler?.Shutdown();
        }
    }
}
