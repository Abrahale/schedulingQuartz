using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace schedulingQuartz
{
    public class SQJob : IJob
    {
        private readonly ILogger<SQJob> _logger;

        public SQJob(ILogger<SQJob> logger) {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Log Hello world!");
            return Task.CompletedTask;
        }
    }
}
