using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl.AdoJobStore.Common;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace schedulingQuartz
{
    public class SQHostingService : IHostedService
    {
       // private readonly IScheduler _scheduler;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly SQJobMetadata _jobMetadata;
        private IScheduler Scheduler { get; set; }

        public SQHostingService(ISchedulerFactory scheduler, IJobFactory jobFactory, SQJobMetadata jobMetadata) {
            _schedulerFactory = scheduler;
            _jobFactory = jobFactory;
            _jobMetadata = jobMetadata;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //     await _scheduler?.Start(cancellationToken);
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;
            var job = CreateJob(_jobMetadata);
            var trigger = CreateTrigger(_jobMetadata);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //    await _scheduler?.Shutdown(cancellationToken);
            await Scheduler?.Shutdown(cancellationToken);
        }

        private ITrigger CreateTrigger(SQJobMetadata sqjb) {
            return TriggerBuilder.Create()
                .WithIdentity(sqjb.JobId.ToString())
                .WithCronSchedule(sqjb.CronExpression)
                .WithDescription($"{sqjb.JobName}")
                .Build();
        }

        private IJobDetail CreateJob(SQJobMetadata sqjMetadata) {
            return JobBuilder
                .Create(sqjMetadata.JobType)
                .WithIdentity(sqjMetadata.JobId.ToString())
                .WithDescription($"{sqjMetadata.JobName}")
                .Build();
        }
    }
}
